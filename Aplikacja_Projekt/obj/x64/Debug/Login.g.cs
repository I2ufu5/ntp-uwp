﻿#pragma checksum "C:\Users\Rufus\Downloads\Aplikacja_Projekt_v2\Aplikacja_Projekt\Aplikacja_Projekt\Login.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "BC087C2BA28D1EC0D67849A8AE0616E1"
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
    partial class Login : 
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
                    this.LoginTextBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 2:
                {
                    this.PasswordTextBox = (global::Windows.UI.Xaml.Controls.PasswordBox)(target);
                }
                break;
            case 3:
                {
                    this.revealModeCheckBox = (global::Windows.UI.Xaml.Controls.CheckBox)(target);
                    #line 66 "..\..\..\Login.xaml"
                    ((global::Windows.UI.Xaml.Controls.CheckBox)this.revealModeCheckBox).Checked += this.CheckBox_Changed;
                    #line 67 "..\..\..\Login.xaml"
                    ((global::Windows.UI.Xaml.Controls.CheckBox)this.revealModeCheckBox).Unchecked += this.CheckBox_Changed;
                    #line default
                }
                break;
            case 4:
                {
                    this.LoginButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 74 "..\..\..\Login.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.LoginButton).Click += this.LoginButton_Click;
                    #line default
                }
                break;
            case 5:
                {
                    this.PopupLog = (global::Windows.UI.Xaml.Controls.Primitives.Popup)(target);
                }
                break;
            case 6:
                {
                    this.RegisterLinkButton = (global::Windows.UI.Xaml.Controls.HyperlinkButton)(target);
                    #line 94 "..\..\..\Login.xaml"
                    ((global::Windows.UI.Xaml.Controls.HyperlinkButton)this.RegisterLinkButton).Click += this.RegisterLinkButton_Click;
                    #line default
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

