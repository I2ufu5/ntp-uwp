﻿#pragma checksum "C:\Users\Rufus\Downloads\Aplikacja_Projekt_v3\Aplikacja_Projekt\Aplikacja_Projekt\AdminPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A0B988AF94143A720F4EA571BADB8401"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Aplikacja_Projekt
{
    partial class AdminPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.DeviceListSource = (global::Windows.UI.Xaml.Data.CollectionViewSource)(target);
                }
                break;
            case 2:
                {
                    this.redeemButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 166 "..\..\..\AdminPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.redeemButton).Click += this.redeemButton_Click;
                    #line default
                }
                break;
            case 3:
                {
                    this.Ilosc3 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 4:
                {
                    this.Ilosc2 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 5:
                {
                    this.Ilosc1 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 6:
                {
                    this.ImieNazwisko = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 7:
                {
                    this.NumerID = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 8:
                {
                    this.OrderList = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    #line 113 "..\..\..\AdminPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.ListView)this.OrderList).SelectionChanged += this.ListView1_SelectionChanged;
                    #line default
                }
                break;
            case 9:
                {
                    this.listUpdateButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 84 "..\..\..\AdminPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.listUpdateButton).Click += this.OrderListUpdate_Click;
                    #line default
                }
                break;
            case 10:
                {
                    this.commandStart = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 73 "..\..\..\AdminPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.commandStart).Click += this.commandStart_Click;
                    #line default
                }
                break;
            case 11:
                {
                    this.commandStop = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 74 "..\..\..\AdminPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.commandStop).Click += this.commandStop_Click;
                    #line default
                }
                break;
            case 12:
                {
                    this.commandReset = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 75 "..\..\..\AdminPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.commandReset).Click += this.commandReset_Click;
                    #line default
                }
                break;
            case 13:
                {
                    this.comPortInput = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 56 "..\..\..\AdminPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.comPortInput).Click += this.comPortInput_Click;
                    #line default
                }
                break;
            case 14:
                {
                    this.closeDevice = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 57 "..\..\..\AdminPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.closeDevice).Click += this.closeDevice_Click;
                    #line default
                }
                break;
            case 15:
                {
                    this.echoButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 60 "..\..\..\AdminPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.echoButton).Click += this.echoSend_Click;
                    #line default
                }
                break;
            case 16:
                {
                    this.echoDiode = (global::Windows.UI.Xaml.Shapes.Rectangle)(target);
                }
                break;
            case 17:
                {
                    this.status = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 18:
                {
                    this.ConnectDevices = (global::Windows.UI.Xaml.Controls.ListBox)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

