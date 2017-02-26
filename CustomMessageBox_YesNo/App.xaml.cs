using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CustomMessageBox_YesNo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {

            MessageBox_YesNo MB_YesNo = new MessageBox_YesNo();
            VM_CustomMessageBox_YesNo VM_YesNo = new VM_CustomMessageBox_YesNo("Test header", "test messsage YES NO");
            MB_YesNo.DataContext = VM_YesNo;

            if ((bool)MB_YesNo.ShowDialog())
            {

            }
        }
    }
}
