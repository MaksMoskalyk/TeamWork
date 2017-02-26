using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Editor_Projects
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            V_Editor_Projects WinEdPr = new V_Editor_Projects();
            VM_Editor_Projects VM_EdPr = new VM_Editor_Projects();
            WinEdPr.DataContext = VM_EdPr;
            WinEdPr.ShowDialog();
        }
    }
}
