﻿#pragma checksum "..\..\..\..\Forme\DodajRacun.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "771C64A02CCDB1BEA41E14833884417BEB04CFB7"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using Zubna_Ordinacija.cs.Forme;


namespace Zubna_Ordinacija.cs.Forme {
    
    
    /// <summary>
    /// DodajRacun
    /// </summary>
    public partial class DodajRacun : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\..\Forme\DodajRacun.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtCena;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\..\Forme\DodajRacun.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnSacuvaj;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\..\Forme\DodajRacun.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnZatvori;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\..\Forme\DodajRacun.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbPacijent;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.5.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Zubna Ordinacija.cs;component/forme/dodajracun.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Forme\DodajRacun.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.txtCena = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.BtnSacuvaj = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\..\..\Forme\DodajRacun.xaml"
            this.BtnSacuvaj.Click += new System.Windows.RoutedEventHandler(this.BtnSacuvaj_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.BtnZatvori = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\..\..\Forme\DodajRacun.xaml"
            this.BtnZatvori.Click += new System.Windows.RoutedEventHandler(this.BtnZatvori_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.cbPacijent = ((System.Windows.Controls.ComboBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

