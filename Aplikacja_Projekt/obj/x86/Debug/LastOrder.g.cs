﻿#pragma checksum "C:\Users\Rufus\Downloads\Aplikacja_Projekt_v3\Aplikacja_Projekt\Aplikacja_Projekt\LastOrder.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "35B34409A0925546FEB9890C39F9470D"
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
    partial class LastOrder : 
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
                    global::Windows.UI.Xaml.Controls.Button element1 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 33 "..\..\..\LastOrder.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)element1).Click += this.OrderListUpdate_Click;
                    #line default
                }
                break;
            case 2:
                {
                    this.OrderList = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    #line 58 "..\..\..\LastOrder.xaml"
                    ((global::Windows.UI.Xaml.Controls.ListView)this.OrderList).SelectionChanged += this.ListView1_SelectionChanged;
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

