﻿#pragma checksum "C:\Users\Rufus\Downloads\Aplikacja_Projekt_v2\Aplikacja_Projekt\Aplikacja_Projekt\Register.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C053DD15D99B7AACCD394C389F44D58E"
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
    partial class Register : 
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
                    this.ImieTextBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 2:
                {
                    this.NazwiskoTextBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 3:
                {
                    this.LoginTBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 4:
                {
                    this.HasloTextBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 5:
                {
                    this.EmailTextBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 6:
                {
                    this.RegisterButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 113 "..\..\..\Register.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.RegisterButton).Click += this.RegisterButton_Click;
                    #line default
                }
                break;
            case 7:
                {
                    this.BackButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 123 "..\..\..\Register.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.BackButton).Click += this.BackButton_Click;
                    #line default
                }
                break;
            case 8:
                {
                    this.PopupLogin = (global::Windows.UI.Xaml.Controls.Primitives.Popup)(target);
                }
                break;
            case 9:
                {
                    this.PopupHaslo = (global::Windows.UI.Xaml.Controls.Primitives.Popup)(target);
                }
                break;
            case 10:
                {
                    this.PopupEmail = (global::Windows.UI.Xaml.Controls.Primitives.Popup)(target);
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

