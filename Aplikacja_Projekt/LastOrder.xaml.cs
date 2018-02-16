using System;
using System.Collections.ObjectModel;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Storage.Streams;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Dynamic;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Aplikacja_Projekt
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LastOrder : Page
    {
        private ObservableCollection<Order> Orders;

        public LastOrder()
        {
            this.InitializeComponent();
            Orders = new ObservableCollection<Order>();
        }

        public async void PostOrderListReq()
        {
            Uri requestUri = new Uri("https://ntpprojekt.000webhostapp.com/historiazamowien.php");
            dynamic dynamicJson = new ExpandoObject();
            dynamicJson.ID_uzytkownika = App.ZmiennaId;
            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(dynamicJson);

            StringContent stringContent = new StringContent(json, System.Text.UnicodeEncoding.UTF8, "application/x-www-form-urlencoded");

            string myContent = stringContent.ReadAsStringAsync().Result;
            Debug.Write("\n" + myContent + "\n");
            var objClint = new System.Net.Http.HttpClient();

            var respon = await objClint.PostAsync(requestUri, stringContent);
            Debug.Write("\n" + respon + "\n");
            if (respon.Content != null)
            {
                var responseContent = await respon.Content.ReadAsStringAsync();
                JObject obj = JObject.Parse(responseContent);
                Debug.Write("\n" + obj.SelectToken("emp_info") + "\n");
                JArray result = JArray.Parse(Convert.ToString(obj.SelectToken("emp_info")));
                for (int i = 0; i < result.Count; i++)
                {
                    Orders.Add(new Order()
                    {
                        order_quantity1 = (string)result[i]["Ilosc_Produktu_1"],
                        order_quantity2 = (string)result[i]["Ilosc_Produktu_2"],
                        order_quantity3 = (string)result[i]["Ilosc_Produktu_3"],
                        order_redeem = (string)result[i]["Data_Realizacji"],
                        order_date = (string)result[i]["Data_Zlozenia"]
                    });
                }

            }


        }

        private void OrderListUpdate_Click(object sender, RoutedEventArgs e)
        {
            Orders.Clear();
            PostOrderListReq();
            OrderList.ItemsSource = Orders;
        }

        private void ListView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        internal class Order
        {
            public string order_quantity1 { get; set; }
            public string order_quantity2 { get; set; }
            public string order_quantity3 { get; set; }
            public string order_redeem { get; set; }
            public string order_date { get; set; }
        }

    }
}