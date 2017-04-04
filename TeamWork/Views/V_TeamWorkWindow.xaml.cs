using System;
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
        private ISaveBeforClose vMSave;
        private MetroWindow accentThemeWindow;

        internal ISaveBeforClose VMSave
        {
            get
            {
                return vMSave;
            }

            set
            {
                vMSave = value;
            }
        }

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
            ClientSettings Settings = new ClientSettings();
            Settings.DeserializeSetting();
            Settings.SetSetting(this);
            GridR.Visibility = Visibility.Collapsed;
            GridL.Visibility = Visibility.Collapsed;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void bR_Click(object sender, RoutedEventArgs e)
        {
            if (GridR.Visibility == Visibility.Visible)
                GridR.Visibility = Visibility.Collapsed;
            else
                GridR.Visibility = Visibility.Visible;
        }
        private void bL_Click(object sender, RoutedEventArgs e)
        {
            if (GridL.Visibility == Visibility.Visible)
                GridL.Visibility = Visibility.Collapsed;
            else
                GridL.Visibility = Visibility.Visible;
        }

        private void CheckClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!VMSave.SaveBeforChange())
            {
                e.Cancel = true;
                return;
            }
            var procs = System.Diagnostics.Process.GetProcessesByName("TeamWork");
            foreach (var proc in procs)
            {
                proc.Kill();
            }
        }

    } 
}
