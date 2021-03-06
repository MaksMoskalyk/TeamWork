﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;
using DelegateCommandNS;
using TeamworkDB;

namespace StaffDatabaseUnit
{
    public class ShowEmployeeDataViewModel : ShowEmployeeDataGlueCode
    {
        public ShowEmployeeDataViewModel() { }

        #region Commands declaration
        // Main data commands
        private DelegateCommand bAddEmployee;
        private DelegateCommand bEditEmployee;
        private DelegateCommand bDeleteEmployee;
        private DelegateCommand bUpdate;

        // Search filter commands
        private DelegateCommand bOpenSearch;
        private DelegateCommand bStartSearch;
        #endregion

        #region Commands implementation

        #region Main data commands

        #region Add employee commmnad
        public ICommand ButtonAddEmployee
        {
            get
            {
                if (bAddEmployee == null)
                {
                    bAddEmployee = new DelegateCommand(d => AddEmployee(), d => IsAddEmployeeAvailable());
                }
                return bAddEmployee;
            }
        }

        private void AddEmployee()
        {
            try
            {
                viewFactory = new AddEmployeeViewFactory();
                addEmployeeView = viewFactory.CreateView();

                InputEmployeeDataViewModel inputEmployeeDataViewModel = new InputEmployeeDataViewModel();
                inputEmployeeDataViewModel.LoadData();
                inputEmployeeDataViewModel.employeeAdditionEvent += new NewEmployeeAddition(UpdateView);

                addEmployeeView.Data = inputEmployeeDataViewModel;
                inputEmployeeDataViewModel.CurrentWindow = addEmployeeView;
                addEmployeeView.ShowView();
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }            
        }

        private bool IsAddEmployeeAvailable()
        {           
            return true;
        }
        #endregion

        #region Edit employee commmnad
        public ICommand ButtonEditEmployee
        {
            get
            {
                if (bEditEmployee == null)
                {
                    bEditEmployee = new DelegateCommand(d => EditEmployee(), d => IsEditEmployeeAvailable());
                }
                return bEditEmployee;
            }
        }

        private void EditEmployee()
        {
            try
            {
                viewFactory = new EditEmployeeDataViewFactory();
                editEmployeeView = viewFactory.CreateView();

                InputEmployeeDataViewModel inputEmployeeDataViewModel = new InputEmployeeDataViewModel();
                inputEmployeeDataViewModel.LoadData();
                inputEmployeeDataViewModel.EmployeeData.Employee = currentEmployee.Employee;
                inputEmployeeDataViewModel.CurrentEmployee =
                    new Employee() { Name = currentEmployee.Employee.Name,
                    Surname = currentEmployee.Employee.Surname,
                    DateOfBirth = currentEmployee.Employee.DateOfBirth};
                inputEmployeeDataViewModel.employeeAdditionEvent += new NewEmployeeAddition(UpdateView);

                editEmployeeView.Data = inputEmployeeDataViewModel;
                inputEmployeeDataViewModel.CurrentWindow = editEmployeeView;
                editEmployeeView.ShowView();
                database.LoadDataForEmployeeEdition(inputEmployeeDataViewModel.EmployeeData, currentEmployee);
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }           
        }

        private bool IsEditEmployeeAvailable()
        {
            return (currentEmployee != null);
        }
        #endregion

        #region Delete employee commmnad
        public ICommand ButtonDeleteEmployee
        {
            get
            {
                if (bDeleteEmployee == null)
                {
                    bDeleteEmployee = new DelegateCommand(d => DeleteEmployee(), d => IsDeleteEmployeeAvailable());
                }
                return bDeleteEmployee;
            }
        }

        private void DeleteEmployee()
        {
            try
            {
                string message = "Are you sure that you want to delete employee?";
                string caption = "Warning!";

                if (messageBox.GetActionConfirmation(message, caption))
                {
                    database.DeleteEmployee(currentEmployee.Employee);
                    employees.Remove(currentEmployee);
                    if (employees.Count > 0)
                        CurrentEmployee = employees[0];
                }
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }            
        }

        private bool IsDeleteEmployeeAvailable()
        {
            return (currentEmployee != null);
        }
        #endregion

        #region Update view commmnad
        public ICommand ButtonUpdate
        {
            get
            {
                if (bUpdate == null)
                {
                    bUpdate = new DelegateCommand(d => UpdateView(), d => true);
                }
                return bUpdate;
            }
        }

        public void UpdateView()
        {
            try
            {
                Employees = new ObservableCollection<SpecificEmployee>();
                database.LoadEmployeesData(Employees);
                if(employees.Count > 0 && employees != null)
                    CurrentEmployee = employees[0];
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }           
        }
        #endregion

        #endregion

        #region Search filter commands

        #region Open search command
        public ICommand ButtonOpenSearch
        {
            get
            {
                if (bOpenSearch == null)
                {
                    bOpenSearch = new DelegateCommand(d => OpenSearch(), d => IsOpenSearchAvailable());
                }
                return bOpenSearch;
            }
        }

        private void OpenSearch()
        {
            try
            {
                isSearchRequested = !isSearchRequested;
                if (isSearchRequested)
                    VisibilityInfo = "Visible";
                else
                    VisibilityInfo = "Collapsed";
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }

        private bool IsOpenSearchAvailable()
        {
            return true;
        }
        #endregion

        #region Start search command
        public ICommand ButtonStartSearch
        {
            get
            {
                if (bStartSearch == null)
                {
                    bStartSearch = new DelegateCommand(d => StartSearch(), d => IsStartSearchAvailable());
                }
                return bStartSearch;
            }
        }

        private void StartSearch()
        {
            try
            {
                Employees = new ObservableCollection<SpecificEmployee>();
                database.SearchEmployees(employees, selectedPosition, skillsForSearch);

                if(employees.Count > 0)
                    CurrentEmployee = employees[0];
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }

        private bool IsStartSearchAvailable()
        {
            return true;
        }
        #endregion

        #endregion

        #endregion
    }
}