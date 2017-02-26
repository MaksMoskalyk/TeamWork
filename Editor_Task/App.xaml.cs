using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;

namespace Editor_Task
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {

            V_Editor_Task WinEdT = new V_Editor_Task();
            VM_Editor_Tasks VM_EdTask = new VM_Editor_Tasks();
            WinEdT.DataContext = VM_EdTask;
            WinEdT.ShowDialog();
        }
    }
}
