using System;
using Windows.UI.Xaml.Controls;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net.Http;
using System.Dynamic;


namespace Aplikacja_Projekt
{
    public sealed partial class AdminPage : Page
    {
        public async void PostOrderListReq()
        {
            Uri requestUri = new Uri("https://ntpprojekt.000webhostapp.com/zamowieniadorealizacji.php");
            dynamic dynamicJson = new ExpandoObject();
            dynamicJson.Data_Realizacji = true;
            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(dynamicJson);

            StringContent stringContent = new StringContent(json, System.Text.UnicodeEncoding.UTF8, "application/x-www-form-urlencoded");

            string myContent = stringContent.ReadAsStringAsync().Result;

            var objClint = new System.Net.Http.HttpClient();

            var respon = await objClint.PostAsync(requestUri, stringContent);

            Debug.Write("\n" + respon + "\n");
            if (respon.Content != null)
            {
                var responseContent = await respon.Content.ReadAsStringAsync();
                JObject obj = JObject.Parse(responseContent);
                // Debug.Write("\n" + obj.SelectToken("emp_info") + "\n");
                JArray result = JArray.Parse(Convert.ToString(obj.SelectToken("emp_info")));
                for (int i = 0; i < result.Count; i++)
                {
                    Orders.Add(new Order()
                    {
                        order_quantity1 = (string)result[i]["Ilosc_Produktu_1"],
                        order_quantity2 = (string)result[i]["Ilosc_Produktu_2"],
                        order_quantity3 = (string)result[i]["Ilosc_Produktu_3"],
                        order_id = (string)result[i]["ID_Zamowienia"],
                        order_name = (string)result[i]["Imie"],
                        order_secondname = (string)result[i]["Nazwisko"],
                        order_date = (string)result[i]["Data_Zlozenia"]
                    });
                }

            }


        }

        private async void PostOrderDateUpdate()
        {
            Uri requestUri = new Uri("https://ntpprojekt.000webhostapp.com/DataRealizacjiUpdate.php");
            //składanie obiektu
            dynamic dynamicJson = new ExpandoObject();
            dynamicJson.Id_Zamowienia = NumerID.Text;
            //spakowanie do jsona
            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(dynamicJson);
            //konwersja do stringa
            StringContent stringContent = new StringContent(json, System.Text.UnicodeEncoding.UTF8, "application/x-www-form-urlencoded");
            string myContent = stringContent.ReadAsStringAsync().Result;
         //   Debug.Write("\n" + myContent + "\n");
            var objClint = new System.Net.Http.HttpClient();

            var respon = await objClint.PostAsync(requestUri, stringContent);
          //  Debug.Write("\n" + respon + "\n");

            if (respon.Content != null)
            {
                var responseContent = await respon.Content.ReadAsStringAsync();
                JObject obj = JObject.Parse(responseContent);
                Debug.Write("\n" + obj.SelectToken("success") + "\n");

            }
        }

        private async void PostSensorStateSend(String sensorID,String sensorVal)
        {
            Uri requestUri = new Uri("https://ntpprojekt.000webhostapp.com/odczytyupdate.php");

            //składanie obiektu
            dynamic dynamicJson = new ExpandoObject();
            dynamicJson.Odczyt = sensorVal;
            if (NumerID.Text == "NULL")
            { dynamicJson.ID_zamowienia = "0"; }
            else
            { dynamicJson.ID_zamowienia = NumerID.Text; }

            dynamicJson.ID_Urzadzenia = sensorID;

            //spakowanie do jsona
            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(dynamicJson);

            //konwersja do stringa
            StringContent stringContent = new StringContent(json, System.Text.UnicodeEncoding.UTF8, "application/x-www-form-urlencoded");
            string myContent = stringContent.ReadAsStringAsync().Result;
            Debug.Write("\n" + myContent + "\n")
                //nowy obiekt do obsługi http
            var objClint = new System.Net.Http.HttpClient();

            var respon = await objClint.PostAsync(requestUri, stringContent);
            Debug.Write("\n" + respon + "\n");

            if (respon.Content != null)
            {
                var responseContent = await respon.Content.ReadAsStringAsync();
                try
                {
                    JObject obj = JObject.Parse(responseContent);
                    Debug.Write("\n" + obj.SelectToken("success") + "\n");
                }catch(Exception ex2) { Debug.Write(ex2); }

            }
        }
    }
}
