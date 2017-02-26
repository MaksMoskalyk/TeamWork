using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Authorization;

namespace TeamWork
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            V_AuthorizationWindow WinAuth = new V_AuthorizationWindow();
            VM_Login Log = new VM_Login();
            WinAuth.DataContext = Log;

            V_TeamWorkWindow WinTW = new V_TeamWorkWindow();
            VM_TeamWork TeamWork = new VM_TeamWork();
            WinTW.DataContext = TeamWork;

            if ((bool)WinAuth.ShowDialog())
            {
                TeamWork.Login = Log.CurrentLogin;                
                WinTW.ShowDialog();
            }
            else
            {
                WinTW.Close();
            }
            
        }
    }
}
