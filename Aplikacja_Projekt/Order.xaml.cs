using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Dynamic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Aplikacja_Projekt
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Order : Page
    {
        private int liczba_cukierkow1 = 0;
        private int liczba_cukierkow2 = 0;
        private int liczba_cukierkow3 = 0;
        private string l_cukierkow1;
        private string l_cukierkow2;
        private string l_cukierkow3;

        public string userid;

        public void Visible()
        {
            if (liczba_cukierkow1 == 0 && liczba_cukierkow2 == 0 && liczba_cukierkow3 == 0) { OrderButton.Visibility = Visibility.Collapsed; }
            else { OrderButton.Visibility = Visibility.Visible; }
        }

        public Order()
        {
            this.InitializeComponent();
            Visible();
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            userid = e.Parameter as string;
        }

        private void AddCukierek1Button_Click(object sender, RoutedEventArgs e)
        {
            PopupOrder.IsOpen = false;
            liczba_cukierkow1 = liczba_cukierkow1 + 1;
            l_cukierkow1 = liczba_cukierkow1.ToString();
            if (liczba_cukierkow1 >= 0) { AllCukierek1.Text = l_cukierkow1; }
            else
            {
                AllCukierek1.Text = "0";
                liczba_cukierkow1 = 0;
            }
            Visible();
        }

        private void AddCukierek2Button_Click(object sender, RoutedEventArgs e)
        {
            PopupOrder.IsOpen = false;
            liczba_cukierkow2 = liczba_cukierkow2 + 1;
            l_cukierkow2 = liczba_cukierkow2.ToString();
            if (liczba_cukierkow2 >= 0) { AllCukierek2.Text = l_cukierkow2; }
            else
            {
                AllCukierek2.Text = "0";
                liczba_cukierkow2 = 0;
            }
            Visible();
        }

        private void AddCukierek3Button_Click(object sender, RoutedEventArgs e)
        {
            PopupOrder.IsOpen = false;
            liczba_cukierkow3 = liczba_cukierkow3 + 1;
            l_cukierkow3 = liczba_cukierkow3.ToString();
            if (liczba_cukierkow3 >= 0) { AllCukierek3.Text = l_cukierkow3; }
            else
            {
                AllCukierek3.Text = "0";
                liczba_cukierkow3 = 0;
            }
            Visible();
        }


        private void SubCukierek1Button_Click(object sender, RoutedEventArgs e)
        {
            PopupOrder.IsOpen = false;
            liczba_cukierkow1 = liczba_cukierkow1 - 1;
            l_cukierkow1 = liczba_cukierkow1.ToString();
            if (liczba_cukierkow1 >= 0) { AllCukierek1.Text = l_cukierkow1; }
            else
            {
                AllCukierek1.Text = "0";
                liczba_cukierkow1 = 0;
            }
            Visible();
        }


        private void SubCukierek2Button_Click(object sender, RoutedEventArgs e)
        {
            PopupOrder.IsOpen = false;
            liczba_cukierkow2 = liczba_cukierkow2 - 1;
            l_cukierkow2 = liczba_cukierkow2.ToString();
            if (liczba_cukierkow2 >= 0) { AllCukierek2.Text = l_cukierkow2; }
            else
            {
                AllCukierek2.Text = "0";
                liczba_cukierkow2 = 0;
            }
            Visible();
        }


        private void SubCukierek3Button_Click(object sender, RoutedEventArgs e)
        {
            PopupOrder.IsOpen = false;
            liczba_cukierkow3 = liczba_cukierkow3 - 1;
            l_cukierkow3 = liczba_cukierkow3.ToString();
            if (liczba_cukierkow3 >= 0) { AllCukierek3.Text = l_cukierkow3; }
            else
            {
                AllCukierek3.Text = "0";
                liczba_cukierkow3 = 0;
            }
            Visible();
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            POSTreq();
            AllCukierek1.Text = "0";
            liczba_cukierkow1 = 0;
            AllCukierek2.Text = "0";
            liczba_cukierkow2 = 0;
            AllCukierek3.Text = "0";
            liczba_cukierkow3 = 0;
            Visible();
        }

        private void ZeroButton_Click(object sender, RoutedEventArgs e)
        {
            PopupOrder.IsOpen = false;
            AllCukierek1.Text = "0";
            liczba_cukierkow1 = 0;
            AllCukierek2.Text = "0";
            liczba_cukierkow2 = 0;
            AllCukierek3.Text = "0";
            liczba_cukierkow3 = 0;
            Visible();
        }

        public async void POSTreq()
        {
            Uri requestUri = new Uri("https://ntpprojekt.000webhostapp.com/skladaniezamowien.php");
            dynamic dynamicJson = new ExpandoObject();            
            dynamicJson.userID = App.ZmiennaId; ;
            dynamicJson.productCount1 = AllCukierek1.Text;
            dynamicJson.productCount2 = AllCukierek2.Text;
            dynamicJson.productCount3 = AllCukierek3.Text;
            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(dynamicJson);

            StringContent stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/x-www-form-urlencoded");

            string myContent = stringContent.ReadAsStringAsync().Result;

            Debug.Write("\n" + myContent + "\n");

            var objClint = new System.Net.Http.HttpClient();

            var respon = await objClint.PostAsync(requestUri, stringContent); 
            Debug.Write("\n" + respon + "\n");
            if (respon.Content != null)
            {
                var responseContent = await respon.Content.ReadAsStringAsync();
                PopupOrder.IsOpen = true;
                
                Debug.Write("\n" + responseContent + "\n");
            }else PopupOrder.IsOpen = false;


        }

    }
}
