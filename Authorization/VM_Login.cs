using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using DelegateCommandNS;
using StaffDatabaseUnit;
using TeamworkDB;
using TeamworkDBEntity;
using VM_Base;
using System.Text.RegularExpressions;

namespace Authorization
{
    public class VM_Login : ViewModelBase
    {
        private string currentEmployee;
        private IDatabaseAuthentication database;
        private string _currentLogin;
        private string _currentPass;
        private string szError;
        public VM_Login()
        {
            _currentPass = "";
            _currentLogin = "";
            database = new DatabaseAuthenticationEntity();
        }
       
        public string CurrentLogin
        {
            get
            {
                return _currentLogin;
            }
            set
            {
                _currentLogin = value;
                
            }
        }

        public string CurrentEmployee
        {
            get
            {
                return currentEmployee;
            }
            set
            {
                currentEmployee = value;

            }
        }

        public string TxError
        {
            get
            {
                return szError;
            }
            set
            {
                szError = value;
                OnPropertyChanged("TxError");
            }
        }

        public string CurrentPass
        {
            get
            {
                return _currentPass;
            }
            set
            {
                _currentPass = value;
               
            }
        }
        private bool? dialogResult;
        public bool? DialogResult
        {
            get { return dialogResult; }
            set
            {
                if (value != this.dialogResult)
                {
                    this.dialogResult = value;
                    OnPropertyChanged("DialogResult");
                }
            }
        }


        private DelegateCommand ButtonEnterClick;
        public ICommand BEnter_Click
        {
            get
            {
                if (ButtonEnterClick == null)
                {
                    ButtonEnterClick = new DelegateCommand(param => this.CheckLog(), param => IsLogInAvailable());
                }
                return ButtonEnterClick;
            }
        }

        void CheckLog()
        {
            
            Employee Employee = database.GetEmployeeByAccount(_currentLogin, _currentPass);

            if (Employee != null)
            {
                DialogResult = true;
            }
            else
            {
                TxError = "Account with such login and password wasn't found!";
            }
        }

        bool IsLogInAvailable()
        {
            return (CurrentPass.Trim().Length >0  && CurrentPass.Trim().Length > 0);
        }
    }
}

