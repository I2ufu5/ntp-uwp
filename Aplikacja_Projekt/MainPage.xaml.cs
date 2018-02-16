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



// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Aplikacja_Projekt
{ 
   
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        System.Threading.Timer _timer;
        public MainPage()
        {
            this.InitializeComponent();
            MyFrame.Navigate(typeof(Login));
            _timer = new System.Threading.Timer(new System.Threading.TimerCallback((obj) => Refresh()), null, 0, 5000);
        }

        private async void Refresh()
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
            () =>
            {
                UserIdTextBlock.Text = App.ZmiennaId;
            });
        }


        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OrderListBoxItem.IsSelected)
            {
                if (App.ZmiennaId == "") { }
                else
                {
                    PopupAdmin.IsOpen = false;
                    MyFrame.Navigate(typeof(Order));
                    IdChange();
                }
            }
            else if (LastOrderListBoxItem.IsSelected)
            {
                if (App.ZmiennaId == "") { }
                else
                {
                    PopupAdmin.IsOpen = false;
                    MyFrame.Navigate(typeof(LastOrder));
                    IdChange();
                }
            }
            else if(AdminListBoxItem.IsSelected)
            {
                if(App.ZmiennaId == "") { }
                else if (App.ZmiennaAdmin == "0") { PopupAdmin.IsOpen = true; }
                else
                {
                    if(App.ZmiennaAdmin != "0")
                    {
                        PopupAdmin.IsOpen = false;
                        MyFrame.Navigate(typeof(AdminPage));
                        IdChange();
                    }
                    
                }
            }
       }
        

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            
            MyFrame.Navigate(typeof(Login));
            App.ZmiennaId = "";
            App.ZmiennaLogin = "";
            App.ZmiennaEmail = "";
            App.ZmiennaAdmin = "0";
            IdChange();
            Debug.Write("\n" + App.ZmiennaId + "\n");
            Debug.Write("\n" + App.ZmiennaLogin + "\n");
            Debug.Write("\n" + App.ZmiennaEmail + "\n");
            Debug.Write("\n" + App.ZmiennaAdmin + "\n");
        }

       
        public void IdChange()
        {
            UserIdTextBlock.Text = App.ZmiennaId;            
        }
        
    }
}
