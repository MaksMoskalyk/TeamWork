using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Authorization
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

            if ((bool)WinAuth.ShowDialog())
            {

            }
            else
            {

            }



        }
    }
}
