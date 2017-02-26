using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Chat_Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {

            VM_Chat viewModel = new VM_Chat();
            ChatWindow view = new ChatWindow();
            view.DataContext = viewModel;
            if (view.ShowDialog().Value) { }
            viewModel.ClosedChat();
        }
    }
}
