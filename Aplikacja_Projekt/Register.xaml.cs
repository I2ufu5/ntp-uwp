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
    public sealed partial class Register : Page
    {
        public Register()
        {
            this.InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (ImieTextBox.Text == "" || NazwiskoTextBox.Text == "" || LoginTBox.Text == "" || HasloTextBox.Text == "" || EmailTextBox.Text == "" || LoginTBox.Text.Length <= 5 || !EmailTextBox.Text.Contains(".") || !EmailTextBox.Text.Contains("@") || HasloTextBox.Text.Length <= 5)
            {
                if (ImieTextBox.Text == "") { ImieTextBox.PlaceholderText = "Puste pole!"; }
                if (NazwiskoTextBox.Text == "") { NazwiskoTextBox.PlaceholderText = "Puste pole!"; }
                if (LoginTBox.Text == "") { LoginTBox.PlaceholderText = "Puste pole!"; }
                if (HasloTextBox.Text == "") { HasloTextBox.PlaceholderText = "Puste pole!"; }
                if (EmailTextBox.Text == "") { EmailTextBox.PlaceholderText = "Puste pole!"; }
                if (LoginTBox.Text.Length <= 5) PopupLogin.IsOpen = true; else PopupLogin.IsOpen = false;
                if (HasloTextBox.Text.Length <= 5) PopupHaslo.IsOpen = true; else PopupHaslo.IsOpen = false;
                if (!EmailTextBox.Text.Contains("@") || !EmailTextBox.Text.Contains(".")) PopupEmail.IsOpen = true; else PopupEmail.IsOpen = false;
            }
            else if (EmailTextBox.Text.Contains("@") && !EmailTextBox.Text.Contains(".") && HasloTextBox.Text.Length > 5 && LoginTBox.Text.Length > 5 && ImieTextBox.Text != "" && NazwiskoTextBox.Text != "")
            { POSTreq(); }

        }

        public async void POSTreq()
        {
            Uri requestUri = new Uri("https://ntpprojekt.000webhostapp.com/Register.php");
            dynamic dynamicJson = new ExpandoObject();
            dynamicJson.name = ImieTextBox.Text;
            dynamicJson.secondname = NazwiskoTextBox.Text;
            dynamicJson.login = LoginTBox.Text;
            dynamicJson.password = HasloTextBox.Text;
            dynamicJson.email = EmailTextBox.Text;
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
                JObject obj = JObject.Parse(responseContent);
                var z1 = (string)obj.SelectToken("success");                
                
                if (z1 == "True")
                {
                    Frame.Navigate(typeof(Login));
                }
            }

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Login));
        }
    }
}
