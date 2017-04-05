using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CustomMessageBox;
using MahApps.Metro;
using MahApps.Metro.Controls;


namespace AccentStyle
{
    /// <summary>
    /// Interaction logic for AccentStyleWindow.xaml
    /// </summary>
    public partial class AccentStyleWindow : MetroWindow
    {
        ClientSettings Settings = new ClientSettings();
        public static readonly DependencyProperty ColorsProperty
            = DependencyProperty.Register("Colors",
                                          typeof(List<KeyValuePair<string, Color>>),
                                          typeof(AccentStyleWindow),
                                          new PropertyMetadata(default(List<KeyValuePair<string, Color>>)));

        public List<KeyValuePair<string, Color>> Colors
        {
            get { return (List<KeyValuePair<string, Color>>)GetValue(ColorsProperty); }
            set { SetValue(ColorsProperty, value); }
        }

        public AccentStyleWindow()
        {
            InitializeComponent();

            this.DataContext = this;

            this.Colors = typeof(Colors)
                .GetProperties()
                .Where(prop => typeof(Color).IsAssignableFrom(prop.PropertyType))
                .Select(prop => new KeyValuePair<String, Color>(prop.Name, (Color)prop.GetValue(null)))
                .ToList();

            var theme = ThemeManager.DetectAppStyle(Application.Current);
            ThemeManager.ChangeAppStyle(this, theme.Item2, theme.Item1);
        }

        private void ChangeAppThemeButtonClick(object sender, RoutedEventArgs e)
        {
            var theme = ThemeManager.DetectAppStyle(this);
            ThemeManager.ChangeAppStyle(this, theme.Item2, ThemeManager.GetAppTheme("Base" + ((Button)sender).Content));

            theme = ThemeManager.DetectAppStyle(Application.Current);
            ThemeManager.ChangeAppStyle(Application.Current, theme.Item2, ThemeManager.GetAppTheme("Base" + ((Button)sender).Content));
            Settings = new ClientSettings(ThemeManager.GetAppTheme("Base" + ((Button)sender).Content).Name, theme.Item2.Name);
            Settings.SerializeSetting();
        }

        private void ChangeAppAccentButtonClick(object sender, RoutedEventArgs e)
        {
            var theme = ThemeManager.DetectAppStyle(this);
            ThemeManager.ChangeAppStyle(this, ThemeManager.GetAccent(((Button)sender).Content.ToString()), theme.Item1);

            theme = ThemeManager.DetectAppStyle(Application.Current);
            ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.GetAccent(((Button)sender).Content.ToString()), theme.Item1);
            Settings = new ClientSettings(theme.Item1.Name, ThemeManager.GetAccent(((Button)sender).Content.ToString()).Name);
            Settings.SerializeSetting();
        }

        private void AccentSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedAccent = AccentSelector.SelectedItem as Accent;
            if (selectedAccent != null)
            {
                var theme = ThemeManager.DetectAppStyle(this);
                ThemeManager.ChangeAppStyle(this, selectedAccent, theme.Item1);

                theme = ThemeManager.DetectAppStyle(Application.Current);
                ThemeManager.ChangeAppStyle(Application.Current, selectedAccent, theme.Item1);
    
                Settings = new ClientSettings(theme.Item1.Name, selectedAccent.Name);
                Settings.SerializeSetting();

            }
        }

        private void ColorsSelectorOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedColor = this.ColorsSelector.SelectedItem as KeyValuePair<string, Color>?;
            if (selectedColor.HasValue)
            {
                var theme = ThemeManager.DetectAppStyle(this);
                ThemeManagerHelper.CreateAppStyleBy(selectedColor.Value.Value, true,this);
                ThemeManagerHelper.CreateAppStyleBy(selectedColor.Value.Value, true);
           
                Settings = new ClientSettings(theme.Item1.Name, selectedColor.Value.Value);
                Settings.SerializeSetting();

            }
        }

        private void saveBeforClosed(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CustomMessageBox.MessageBox MB_YesNo = new CustomMessageBox.MessageBox();
            VM_CustomMessageBox VM_YesNo = new VM_CustomMessageBox("Save style", "Do you want save this style setting?");
            MB_YesNo.DataContext = VM_YesNo;
            if ((bool)MB_YesNo.ShowDialog())
            {
                Settings.SerializeSetting();
                Settings.SerializeSettingTemp();
            }
            else
            {
                Settings.DeserializeSettingTemp();
                Settings.SerializeSetting();
            }
            
            
        }
    }
}
