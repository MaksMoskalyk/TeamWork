using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AccentStyle;
using MahApps.Metro.Controls;


namespace StaffDatabaseUnit
{
    /// <summary>
    /// Interaction logic for ChangeAccountInfoView.xaml
    /// </summary>
    public partial class ChangeAccountInfoView : MetroWindow, IView
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
            if ((bool)accentThemeWindow.ShowDialog()) { }
            ClientSettings Settings = new ClientSettings();
            Settings.DeserializeSetting();
            Settings.SetSetting(this);
        }

        public ChangeAccountInfoView()
        {
            InitializeComponent();
            ClientSettings Settings = new ClientSettings();
            Settings.DeserializeSetting();
            Settings.SetSetting(this);
        }

        public void ShowView()
        {
            this.Show();
        }

        public void CloseView()
        {
            this.Close();
        }

        public object Data
        {
            get { return this.DataContext; }
            set { this.DataContext = value; }
        }

        public string Caption
        {
            get { return this.Title; }
            set { this.Title = value; }
        }
    }
}