using System;
using Windows.UI.Xaml.Controls;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using Windows.Storage.Streams;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI;
using System.Diagnostics;


namespace Aplikacja_Projekt
{
    public sealed partial class AdminPage : Page
    {
       
        ///////////// Połączenie//////////////////
        private async void ListAvailablePorts()
        {
            try
            {
                string aqs = SerialDevice.GetDeviceSelector();
                var dis = await DeviceInformation.FindAllAsync(aqs);

                status.Text = "Select a device and connect";

                for (int i = 0; i < dis.Count; i++)
                {
                    listOfDevices.Add(dis[i]);
                }

                DeviceListSource.Source = listOfDevices;
                comPortInput.IsEnabled = true;
                ConnectDevices.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                status.Text = ex.Message;
            }
        }

        ///////////// WYSYŁANIE //////////////////

        private async Task UartSendQuantity(int num1, int num2, int num3)
        {
            Task<UInt32> storeAsyncTask;
            String message = null;
            String[] quantity = new String[3];
            quantity[0] = fillWithChar(num1.ToString(), "0", 3,false);
            quantity[1] = fillWithChar(num2.ToString(), "0", 3,false);
            quantity[2] = fillWithChar(num3.ToString(), "0", 3,false);
            message += "#Q/" + quantity[0] + "/" + quantity[1] + "/" + quantity[2] + "/@";

            message = fillWithChar(message, ".", 16,true);

            Debug.WriteLine("SEND "+message);

            if (message.Length != 0)
            {

                dataWriteObject.WriteString(message);
                storeAsyncTask = dataWriteObject.StoreAsync().AsTask();

                UInt32 bytesWritten = await storeAsyncTask;
                if (bytesWritten > 0)
                {
                    status.Text = "wysłano pomyślnie";
                }
            }
            else
            {
                status.Text = "send nudes";
            }
        }

        private async Task UartSendCommand(String komenda)
        {
            Task<UInt32> storeAsyncTask;
            string message = null;

            message += "#D/" + komenda.ToString() + "/@";

            message = fillWithChar(message, ".", 16,true);

            Debug.WriteLine("SEND " + message);

            if (message.Length != 0)
            {
                dataWriteObject.WriteString(message);
                storeAsyncTask = dataWriteObject.StoreAsync().AsTask();

                UInt32 bytesWritten = await storeAsyncTask;
                if (bytesWritten > 0)
                {
                    status.Text = "wysłano pomyślnie";
                }
            }
            else
            {
                status.Text = "send nudes";
            }
        }

        private async Task UartSendEcho()
        {
            Task<UInt32> storeAsyncTask;
            string message = null;

            message = fillWithChar("#@", ".", 16,true);

            Debug.WriteLine("ECHO" + message);

            if (message.Length != 0)
            {
                dataWriteObject.WriteString(message);
                storeAsyncTask = dataWriteObject.StoreAsync().AsTask();

                UInt32 bytesWritten = await storeAsyncTask;
                if (bytesWritten > 0)
                {
                    status.Text = "wysłano pomyślnie";
                    echoTimer.Start();
                }
            }
            else
            {
                status.Text = "send nudes";
            }
        }
 
        ///////////// ODBIOR //////////////////
        private async void Listen()
        {
            try
            {
                if (serialPort != null)
                {
                    dataReaderObject = new DataReader(serialPort.InputStream);

                    while (true)
                    {
                        await ReadAsync(ReadCancellationTokenSource.Token);
                    }
                }

            }
            catch (TaskCanceledException tce)
            {
                status.Text = "Reading task was cancelled, closing device and cleaning up";
                CloseDevice();
            }
            catch (Exception ex)
            {
                status.Text = ex.Message;
            }
            finally
            {
                if (dataReaderObject != null)
                {
                    dataReaderObject.DetachStream();
                    dataReaderObject = null;
                }
            }
        }

        private async Task ReadAsync(CancellationToken cancellationToken)
        {
            Task<UInt32> loadAsyncTask;
            //String incomingString;
            uint ReadBufferLength = 1024;
            cancellationToken.ThrowIfCancellationRequested();

            // Set InputStreamOptions to complete the asynchronous read operation when one or more bytes is available
            dataReaderObject.InputStreamOptions = InputStreamOptions.Partial;

            using (var childCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
            {
                // Create a task object to wait for data on the serialPort.InputStream
                loadAsyncTask = dataReaderObject.LoadAsync(ReadBufferLength).AsTask(childCancellationTokenSource.Token);

                // Launch the task and wait
                UInt32 bytesRead = await loadAsyncTask;

                if (bytesRead > 0)
                {
                    String stringRead = dataReaderObject.ReadString(bytesRead);
                    //rcvdText.Text = stringRead;
                    AnalyzeIncomingString(stringRead);
                }

            }
        }

        private void AnalyzeIncomingString(String incomingString)
        {
            String incomingHeader = incomingString.Substring(0, 2);
            String incomingBody = incomingString.Substring(2);


            Debug.Write("\n" + incomingHeader + incomingBody + "\n");
           // Debug.Write("\n" + incomingBody + "\n");

            if (incomingHeader == "#Q")
            {//zamowienie zakończone
                bool success;
                String[] subStrings = incomingBody.Split('/');



                int[] tab = new int[3];
                Int32.TryParse(subStrings[1], out tab[0]);
                Int32.TryParse(subStrings[2], out tab[1]);
                Int32.TryParse(subStrings[3], out tab[2]);

                Debug.WriteLine(subStrings[1]);
                Debug.WriteLine(subStrings[2]);
                Debug.WriteLine(subStrings[3]);

                Debug.WriteLine(tab[0]);
                Debug.WriteLine(tab[1]);
                Debug.WriteLine(tab[2]);

                success = (tab[0].ToString() == Ilosc1.Text) && 
                          (tab[1].ToString() == Ilosc2.Text) && 
                          (tab[2].ToString() == Ilosc3.Text) ;

                if (success)
                {
                    //odblokuj pola
                    ManipulationState(true);
                    //wyślij date realizacji
                    PostOrderDateUpdate();
                    //wykonano zamówienie
                    status.Text = "Wykonano zamówienie numer " + NumerID.Text;
                }
            }
            if (incomingHeader == "#S")
            {
                String[] subStrings = incomingBody.Split('/');
                String sensorID = subStrings[1];
                String sensorValue = subStrings[2];
                //wyślij dane sensora
                PostSensorStateSend(sensorID, sensorValue);
                //zmien kolor diody
                hwActivity(sensorID, sensorValue);
            }
            if (incomingHeader == "#@")
            {
                echoDiode.Fill = new SolidColorBrush(Colors.Green);
                status.Text = "Odebrano echo";
                echoTimer.Stop();
                //echoSend_event();
            }
            if (incomingHeader == "#E")
            {
                String[] subStrings = incomingBody.Split('/');
                String brak1 = subStrings[1];
                String brak2 = subStrings[1];
                String brak3 = subStrings[1];
                //wyślij dane sensora
                if(brak1=="1" | brak2 =="1" | brak3=="1")
                {
                    status.Text = "BRAK CUKIERKÓW -> AUTOMAT ZATRZYMANO";
                }
            }
        }

        private void CancelReadTask()
        {
            if (ReadCancellationTokenSource != null)
            {
                if (!ReadCancellationTokenSource.IsCancellationRequested)
                {
                    ReadCancellationTokenSource.Cancel();
                }
            }
        }


        ///////////// Zakończenie połączenia //////////////////

        private void CloseDevice()
        {
            if (serialPort != null)
            {
                serialPort.Dispose();
            }
            serialPort = null;

            comPortInput.IsEnabled = true;
            listOfDevices.Clear();
        }

        ///////////// dodatkowe //////////////////

        private String fillWithChar(String stringToFill, String charToFill, int length, bool back)
        {
            String outputString = null;
            if (back)
            {
                outputString += stringToFill;
                for (int i = 0; i < length - stringToFill.Length; i++)
                    outputString += charToFill;

                return outputString;
            }
            else { 
            for (int i = 0; i < length - stringToFill.Length; i++)
                outputString += charToFill;

            return outputString += stringToFill;
            }
        }

        public void dispatcherTimer_Tick(object sender, object e)
        {
            echoDiode.Fill = new SolidColorBrush(Colors.DarkRed);
            echoTimer.Stop();
        }
    }
}
