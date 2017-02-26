using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MahApps.Metro;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;
using System.Windows.Media;
using System.Windows;
using System.Xml.Serialization;

namespace AccentStyle
{
    [Serializable]
    [DataContract]
    public class ClientSettings
    {
        [DataMember]
        public string appTheme { set; get; }
        [DataMember]
        public string accent { set; get; }
        [DataMember]
        public Color color { set; get; }
        public ClientSettings()
        {
            //appTheme = "BaseLight";
            //accent = "Green";
        }
        public ClientSettings(ClientSettings Settings)
        {
            appTheme = Settings.appTheme;
            accent = Settings.accent;
            color = Settings.color;
        }
        public ClientSettings(string AppTheme, string Accent)
        {
            appTheme = AppTheme;
            accent = Accent;
            color = new Color();
        }
        public ClientSettings(string AppTheme, string Accent, Color Color)
        {
            appTheme = AppTheme;
            accent = Accent;
            color = Color;
        }
        public ClientSettings(string AppTheme, Color Color)
        {
            appTheme = AppTheme;
            accent = null;
            color = Color;
        }
        public void SerializeSetting()
        {
            FileStream stream = new FileStream("../../../Settings/ClientSettings.xml", FileMode.Create);
            XmlSerializer serializer = new XmlSerializer(typeof(ClientSettings));
            ClientSettings Settings = new ClientSettings(appTheme, accent, color);
            serializer.Serialize(stream, Settings);
            stream.Close();

            

        }
        public void DeserializeSetting()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(ClientSettings));
            using (FileStream fs = new FileStream("../../../Settings/ClientSettings.xml", FileMode.OpenOrCreate))
            {
                try
                {
                    var Settings = (ClientSettings)formatter.Deserialize(fs);
                    appTheme = Settings.appTheme;
                    accent = Settings.accent;
                    color = Settings.color;
                }
                catch(System.Runtime.Serialization.SerializationException ex)
                {

                }
            }
        }
        public void SetSetting(Window win)
        {
            try
            {

                if (accent != null)
                    ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.GetAccent(accent), ThemeManager.GetAppTheme(appTheme));
                else
                {
                    var theme = ThemeManager.DetectAppStyle(Application.Current);
                    ThemeManager.ChangeAppStyle(Application.Current, theme.Item2, ThemeManager.GetAppTheme(appTheme));
                    ThemeManagerHelper.CreateAppStyleBy(color, true);
                }
            }
            catch { }

        }
    }
}
