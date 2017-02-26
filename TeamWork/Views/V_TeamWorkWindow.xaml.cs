﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MahApps.Metro.Controls;
using MahApps.Metro;
using System.Windows.Threading;
using System.Windows.Input;
using System.Diagnostics;
using System.Windows.Documents;
using AccentStyle;

namespace TeamWork
{    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class V_TeamWorkWindow : MetroWindow
    {
        private MetroWindow accentThemeWindow;

        private void ChangeAppStyleButtonClick(object sender, RoutedEventArgs e)
        {
            if (accentThemeWindow != null)
            {
                accentThemeWindow.Activate();
                return;
            }
            accentThemeWindow = new AccentStyleWindow();
            accentThemeWindow.Owner = this;
            accentThemeWindow.Closed += (o, args) => accentThemeWindow = null;
            accentThemeWindow.Left = this.Left + this.ActualWidth / 2.0;
            accentThemeWindow.Top = this.Top + this.ActualHeight / 2.0;
            accentThemeWindow.Show();
        }

       
        public V_TeamWorkWindow()
        {
            InitializeComponent();
            //DispatcherTimer timer = new DispatcherTimer();
            //timer.Tick += new EventHandler(timer_Tick);
            //timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            //timer.Start();

            
            ClientSettings Settings = new ClientSettings();
            Settings.DeserializeSetting();
            Settings.SetSetting(this);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }




        //private void timer_Tick(object sender, EventArgs e)
        //{
        //    this.EditBox.UpdateDocumentBindings();
        //}

        //private void BSmile(object sender, RoutedEventArgs e)
        //{
        //    Button btn = sender as Button;
        //    var img = btn.Background as ImageBrush;
        //    string path = img.ImageSource.ToString().Remove(0,8).Replace("/","\\");
        //    EditBox.setImage(path);
        //}

        //private void Send(object sender, RoutedEventArgs e)
        //{
        //    EditBox.ClearRTB();
        //}
    }
   
    
}