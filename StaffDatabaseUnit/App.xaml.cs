using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace StaffDatabaseUnit
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            try
            {
                //DatabaseAdministrationView databaseAdministrationView =
                //    new DatabaseAdministrationView();
                //DatabaseAdministrationViewModel databaseAdministrationViewModel =
                //    new DatabaseAdministrationViewModel();
                //databaseAdministrationViewModel.GetInitialData();
                //databaseAdministrationView.DataContext = databaseAdministrationViewModel;
                //databaseAdministrationView.Show();

                //InputEmployeeDataView inputEmployeeDataView = new InputEmployeeDataView();
                //InputEmployeeDataViewModel inputEmployeeDataViewModel = new InputEmployeeDataViewModel();
                //inputEmployeeDataViewModel.LoadData();
                //inputEmployeeDataView.DataContext = inputEmployeeDataViewModel;
                //inputEmployeeDataView.Show();

                ShowEmployeeDataView showEmployeeDataView = new ShowEmployeeDataView();
                ShowEmployeeDataViewModel showEmployeeDataViewModel = new ShowEmployeeDataViewModel();
                showEmployeeDataView.DataContext = showEmployeeDataViewModel;
                showEmployeeDataView.Show();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
    }
}
