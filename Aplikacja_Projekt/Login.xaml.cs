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
    public sealed partial class Login : Page
    {
        public Login()
        {
            this.InitializeComponent();
            
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            POSTreq();
            
        }


        private void RegisterLinkButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Register));
        }
        

        public async void POSTreq()
        {
            Uri requestUri = new Uri("https://ntpprojekt.000webhostapp.com/Login.php");
            dynamic dynamicJson = new ExpandoObject();
            dynamicJson.login = LoginTextBox.Text;
            dynamicJson.password = PasswordTextBox.Password;
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

                try
                {
                    JObject obj = JObject.Parse(responseContent);
                    var zm1 = (string)obj.SelectToken("success");
                    var zm2 = (string)obj.SelectToken("login");
                    var zm3 = (string)obj.SelectToken("email");
                    var zm4 = (string)obj.SelectToken("userID");
                    var zm5 = (string)obj.SelectToken("Admin");

                    if (zm1 == "True")
                    {
                        PopupLog.IsOpen = false;
                        App.ZmiennaId = zm4;
                        App.ZmiennaLogin = zm2;
                        App.ZmiennaEmail = zm3;
                        App.ZmiennaAdmin = zm5;

                        if (zm5 == "0")
                            Frame.Navigate(typeof(Order));
                        else
                            Frame.Navigate(typeof(AdminPage));

                        Debug.Write("\n" + App.ZmiennaId + "\n");
                    }
                    else { PopupLog.IsOpen = true; }
                        
                }
                catch (Exception ex1)
                {
                    Debug.Write(ex1);
                }
            }
            else
            {
                LoginTextBox.Text = "";
                PasswordTextBox.Password = "";
            }

        }

        private void CheckBox_Changed(object sender, RoutedEventArgs e)
        {
            if (revealModeCheckBox.IsChecked == true)
            {
                PasswordTextBox.PasswordRevealMode = PasswordRevealMode.Visible;
            }
            else
            {
                PasswordTextBox.PasswordRevealMode = PasswordRevealMode.Hidden;
            }
        }

        public void Odswiez()
        {
            
        }
    }
}
