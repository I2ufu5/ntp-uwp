using System;
using System.Collections.ObjectModel;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Storage.Streams;
using System.Threading;
using System.Diagnostics;
using Windows.UI.Xaml.Media;
using Windows.UI;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Aplikacja_Projekt
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdminPage : Page
    {
        private ObservableCollection<Order> Orders;
        private SerialDevice serialPort = null;
        DataWriter dataWriteObject = null;
        DataReader dataReaderObject = null;

        private DispatcherTimer echoTimer = new DispatcherTimer();

        private ObservableCollection<DeviceInformation> listOfDevices;
        private CancellationTokenSource ReadCancellationTokenSource;

        /// <summary>
        /// metody
        /// </summary>
        
        public AdminPage()
        {
             this.InitializeComponent();
             Orders = new ObservableCollection<Order>();
             listOfDevices = new ObservableCollection<DeviceInformation>();
             ListAvailablePorts();

             ManipulationState(false);

             commandStart.IsEnabled = false;
             commandStop.IsEnabled = false;
             commandReset.IsEnabled = false;

             NumerID.Text = "NULL";
             ImieNazwisko.Text = "NULL";
             Ilosc1.Text = "NULL";
             Ilosc2.Text = "NULL";
             Ilosc3.Text = "NULL";
        }

        // buttony 
        private async void comPortInput_Click(object sender, RoutedEventArgs e)
        {
            var selection = ConnectDevices.SelectedItems;

            if (selection.Count <= 0)
            {
                status.Text = "Wybierz COM i połącz";
                return;
            }

            DeviceInformation entry = (DeviceInformation)selection[0];

            try
            {
                serialPort = await SerialDevice.FromIdAsync(entry.Id);
                if (serialPort == null) return;

                // Disable the 'Connect' button 
                comPortInput.IsEnabled = false;

                // Configure serial settings
                serialPort.WriteTimeout = TimeSpan.FromMilliseconds(20);
                serialPort.ReadTimeout = TimeSpan.FromMilliseconds(20);
                serialPort.BaudRate = 9600;
                serialPort.Parity = SerialParity.None;
                serialPort.StopBits = SerialStopBitCount.One;
                serialPort.DataBits = 8;
                serialPort.Handshake = SerialHandshake.None;

                // Display configured settings
                status.Text = "Połączono pomyślnie: ";
                status.Text += serialPort.BaudRate + "-";
                status.Text += serialPort.DataBits + "-";
                status.Text += serialPort.Parity.ToString() + "-";
                status.Text += serialPort.StopBits;

                // ustawienie timera echo
               echoTimer.Tick += dispatcherTimer_Tick;
               echoTimer.Interval = new TimeSpan(0, 0, 1);
              // echoTimer.Start();

                // Create cancellation token object to close I/O operations when closing the device
                ReadCancellationTokenSource = new CancellationTokenSource();

                // Enable 'WRITE' button to allow sending data

                Listen();
                ManipulationState(true);
                commandStart.IsEnabled = true;
                commandStop.IsEnabled = true;
                commandReset.IsEnabled = true;

            }
            catch (Exception ex)
            {
                status.Text = ex.Message;
                Debug.Write(ex.Message);
                comPortInput.IsEnabled = true;
            }
        }

        private void closeDevice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                status.Text = "Zakończono połączenie";
                CancelReadTask();

                ManipulationState(false);
                commandStart.IsEnabled = false;
                commandStop.IsEnabled = false;
                commandReset.IsEnabled = false;

                CloseDevice();
                ListAvailablePorts();
            }
            catch (Exception ex)
            {
                status.Text = ex.Message;
            }
        }

        private async void echoSend_Click(object sender, RoutedEventArgs e)
        {
            echoTimer.Start();
            try
            {
                if (serialPort != null)
                {
                    // Create the DataWriter object and attach to OutputStream
                    dataWriteObject = new DataWriter(serialPort.OutputStream);

                    //Launch the WriteAsync task to perform the write
                    await UartSendEcho();
                    status.Text = "Wysłano echo";
                }
                else
                {
                    status.Text = "Select a device and connect";
                }
            }
            catch (Exception ex)
            {
                status.Text = "echoSend_Click: " + ex.Message;
            }
            finally
            {
                // Cleanup once complete
                if (dataWriteObject != null)
                {
                    dataWriteObject.DetachStream();
                    dataWriteObject = null;
                }
            }

        }

        private async void commandStart_Click(object sender, RoutedEventArgs e)
        {
            echoTimer.Start();
            try
            {
                if (serialPort != null)
                {
                    // Create the DataWriter object and attach to OutputStream
                    dataWriteObject = new DataWriter(serialPort.OutputStream);

                    //Launch the WriteAsync task to perform the write
                    await UartSendCommand("s");
                    status.Text = "Wysłano polecenie wznowienia automatu";
                }
                else
                {
                    status.Text = "Select a device and connect";
                }
            }
            catch (Exception ex)
            {
                status.Text = "ComandSend_Click: " + ex.Message;
            }
            finally
            {
                // Cleanup once complete
                if (dataWriteObject != null)
                {
                    dataWriteObject.DetachStream();
                    dataWriteObject = null;
                }
            }

        }

        private async void commandStop_Click(object sender, RoutedEventArgs e)
        {
            echoTimer.Start();
            try
            {
                if (serialPort != null)
                {
                    // Create the DataWriter object and attach to OutputStream
                    dataWriteObject = new DataWriter(serialPort.OutputStream);

                    //Launch the WriteAsync task to perform the write
                    await UartSendCommand("w");
                    status.Text = "Wysłano polecenie wstrzymania automatu";
                }
                else
                {
                    status.Text = "Select a device and connect";
                }
            }
            catch (Exception ex)
            {
                status.Text = "commandStop_Click " + ex.Message;
            }
            finally
            {
                // Cleanup once complete
                if (dataWriteObject != null)
                {
                    dataWriteObject.DetachStream();
                    dataWriteObject = null;
                }
            }

        }

        private async void commandReset_Click(object sender, RoutedEventArgs e)
        {
            echoTimer.Start();
            try
            {
                if (serialPort != null)
                {
                    // Create the DataWriter object and attach to OutputStream
                    dataWriteObject = new DataWriter(serialPort.OutputStream);

                    //Launch the WriteAsync task to perform the write
                    await UartSendCommand("r");
                    status.Text = "Wysłano polecenie zresetowania automatu";
                    ManipulationState(true);
                }
                else
                {
                    status.Text = "Select a device and connect";
                }
            }
            catch (Exception ex)
            {
                status.Text = "commandReset_Click " + ex.Message;
            }
            finally
            {
                // Cleanup once complete
                if (dataWriteObject != null)
                {
                    dataWriteObject.DetachStream();
                    dataWriteObject = null;
                }
            }

        }

        /// order
        internal class Order
        {
            public string order_quantity1 { get; set; }
            public string order_quantity2 { get; set; }
            public string order_quantity3 { get; set; }
            public string order_id { get; set; }
            public string order_name { get; set; }
            public string order_secondname { get; set; }
            public string order_date { get; set; }
        }

        private void OrderListUpdate_Click(object sender, RoutedEventArgs e)
        {
            Orders.Clear();
            PostOrderListReq();
            OrderList.ItemsSource = Orders;
            status.Text = "Zaktualizowano liste zamowień";
        }

        private void ListView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OrderList.SelectedItem != null)
            {

                Order item = (Order)OrderList.SelectedItem;
                NumerID.Text = item.order_id;
                ImieNazwisko.Text = item.order_name + " " + item.order_secondname;
                Ilosc1.Text = item.order_quantity1;
                Ilosc2.Text = item.order_quantity2;
                Ilosc3.Text = item.order_quantity3;
            }
            else
            {
                NumerID.Text = "NULL";
                ImieNazwisko.Text = "NULL";
                Ilosc1.Text = "NULL";
                Ilosc2.Text = "NULL";
                Ilosc3.Text = "NULL";
            }
        }

        private async void redeemButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (serialPort != null)
                {
                    Debug.WriteLine(Ilosc1.Text);
                    if (Ilosc1.Text == "NULL")
                        status.Text = "Zamówienie nie wybrane. Wybierz zamówienie:";
                    else
                    {
                        // Create the DataWriter object and attach to OutputStream
                        dataWriteObject = new DataWriter(serialPort.OutputStream);


                        //Launch the WriteAsync task to perform the write
                        int q1, q2, q3;

                        Int32.TryParse(Ilosc1.Text, out q1);
                        Int32.TryParse(Ilosc2.Text, out q2);
                        Int32.TryParse(Ilosc3.Text, out q3);
                        await UartSendQuantity(q1, q2, q3);
                        status.Text = "Wysłano zamówienie do automatu";
                        ManipulationState(false);
                    }
                }
                else
                {
                    
                }
            }
            catch (Exception ex)
            {
                status.Text = "redeemButton_Click: " + ex.Message;
            }
            finally
            {
                // Cleanup once complete
                if (dataWriteObject != null)
                {
                    dataWriteObject.DetachStream();
                    dataWriteObject = null;
                }
            }



        }

        /// inne
        private async void echoSend_event()
        {
            echoTimer.Start();
            try
            {
                if (serialPort != null)
                {
                    // Create the DataWriter object and attach to OutputStream
                    dataWriteObject = new DataWriter(serialPort.OutputStream);

        //Launch the WriteAsync task to perform the write
        await UartSendEcho();
    }
                else
                {
                    status.Text = "Select a device and connect";
                }
            }
            catch (Exception ex)
            {
                status.Text = "echoSend_Click: " + ex.Message;
            }
            finally
            {
                // Cleanup once complete
                if (dataWriteObject != null)
                {
                    dataWriteObject.DetachStream();
                    dataWriteObject = null;
                }
            }

        }

        private void ManipulationState(bool state)
        {
            if(state)
            {
                //Wszystkie możliwe okienka do manipulacji zamowieniami odblokuj

                echoButton.IsEnabled = true;
                redeemButton.IsEnabled = true;
                listUpdateButton.IsEnabled = true;
                OrderList.IsEnabled = true;
                Ilosc1.IsHoldingEnabled = true;
                Ilosc2.IsHoldingEnabled = true;
                Ilosc3.IsHoldingEnabled = true;
                ImieNazwisko.IsHoldingEnabled = true;
                NumerID.IsHoldingEnabled = true;
            }
            else
            {
                //Wszystkie możliwe okienka do manipulacji zamowieniami zablokuj

                echoButton.IsEnabled = false;
                redeemButton.IsEnabled = false;
                listUpdateButton.IsEnabled = false;
                OrderList.IsEnabled = false;
                Ilosc1.IsHoldingEnabled = false;
                Ilosc2.IsHoldingEnabled = false;
                Ilosc3.IsHoldingEnabled = false;
                ImieNazwisko.IsHoldingEnabled = false;
                NumerID.IsHoldingEnabled = false;
            }

        }

        private void hwActivity(String sensorID,String sensorValue)
        {
            switch(sensorID)
            {
                case "11":
                    if(sensorValue=="1") dioda11.Fill = new SolidColorBrush(Colors.Green);
                    else dioda11.Fill = new SolidColorBrush(Colors.DarkRed);
                    break;
                case "12":
                    if (sensorValue == "1") dioda12.Fill = new SolidColorBrush(Colors.Green);
                    else dioda12.Fill = new SolidColorBrush(Colors.DarkRed);
                    break;
                case "13":
                    if (sensorValue == "1") dioda13.Fill = new SolidColorBrush(Colors.Green);
                    else dioda13.Fill = new SolidColorBrush(Colors.DarkRed);
                    break;
                case "21":
                    if (sensorValue == "1") dioda21.Fill = new SolidColorBrush(Colors.Green);
                    else dioda21.Fill = new SolidColorBrush(Colors.DarkRed);
                    break;
                case "22":
                    if (sensorValue == "1") dioda22.Fill = new SolidColorBrush(Colors.Green);
                    else dioda22.Fill = new SolidColorBrush(Colors.DarkRed);
                    break;
                case "23":
                    if (sensorValue == "1") dioda23.Fill = new SolidColorBrush(Colors.Green);
                    else dioda23.Fill = new SolidColorBrush(Colors.DarkRed);
                    break;
                case "31":
                    if (sensorValue == "1") dioda31.Fill = new SolidColorBrush(Colors.Green);
                    else dioda31.Fill = new SolidColorBrush(Colors.DarkRed);
                    break;
                case "32":
                    if (sensorValue == "1") dioda32.Fill = new SolidColorBrush(Colors.Green);
                    else dioda32.Fill = new SolidColorBrush(Colors.DarkRed);
                    break;
                case "33":
                    if (sensorValue == "1") dioda33.Fill = new SolidColorBrush(Colors.Green);
                    else dioda33.Fill = new SolidColorBrush(Colors.DarkRed);
                    break;
                case "41":
                    if (sensorValue == "1") dioda41.Fill = new SolidColorBrush(Colors.Green);
                    else dioda41.Fill = new SolidColorBrush(Colors.DarkRed);
                    break;
                case "42":
                    if (sensorValue == "1") dioda42.Fill = new SolidColorBrush(Colors.Green);
                    else dioda42.Fill = new SolidColorBrush(Colors.DarkRed);
                    break;
                case "43":
                    if (sensorValue == "1") dioda43.Fill = new SolidColorBrush(Colors.Green);
                    else dioda43.Fill = new SolidColorBrush(Colors.DarkRed);
                    break;

                case "50":
                    if (sensorValue == "1") dioda50.Fill = new SolidColorBrush(Colors.Green);
                    else dioda50.Fill = new SolidColorBrush(Colors.DarkRed);
                    break;

                case "0":
                    if (sensorValue == "1") dioda00.Fill = new SolidColorBrush(Colors.Green);
                    else dioda00.Fill = new SolidColorBrush(Colors.DarkRed);
                    break;

                case "1":
                    if (sensorValue == "1") dioda01.Fill = new SolidColorBrush(Colors.Green);
                    else dioda01.Fill = new SolidColorBrush(Colors.DarkRed);
                    break;
            }
        }
    }


}
