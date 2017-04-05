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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AccentStyle;
using MahApps.Metro.Controls;

namespace Editor_Task
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class V_Editor_Task : MetroWindow
    {
        public V_Editor_Task()
        {
            InitializeComponent();
            ClientSettings Settings = new ClientSettings();
            Settings.DeserializeSetting();
            Settings.SetSetting(this);
        }
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
    }
}
