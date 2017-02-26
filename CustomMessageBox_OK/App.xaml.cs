using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CustomMessageBox_OK
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {

            MessageBox_OK MB_OK = new MessageBox_OK();
            VM_CustomMessageBox_OK VM_OK = new VM_CustomMessageBox_OK("Test header", "test messsage OK");
            MB_OK.DataContext = VM_OK;

            if ((bool)MB_OK.ShowDialog())
            {

            }



        }
    }
}
