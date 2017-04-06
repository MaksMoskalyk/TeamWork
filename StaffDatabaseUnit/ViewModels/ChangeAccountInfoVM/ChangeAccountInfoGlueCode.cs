using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM_Base;

namespace StaffDatabaseUnit
{
    class ChangeAccountInfoGlueCode : ViewModelBase
    {
        private string login;
        private string password;
        private string newLogin;
        private string newPassword;

        public ChangeAccountInfoGlueCode() { }

        public ChangeAccountInfoGlueCode(string login, string password, string newLogin, string newPassword)
        {
            this.Login = login;
            this.Password = password;
            this.NewLogin = newLogin;
            this.NewPassword = newPassword;
        }

        public string Login
        {
            get
            {
                return login;
            }

            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        public string NewLogin
        {
            get
            {
                return newLogin;
            }

            set
            {
                newLogin = value;
                OnPropertyChanged("NewLogin");
            }
        }

        public string NewPassword
        {
            get
            {
                return newPassword;
            }

            set
            {
                newPassword = value;
                OnPropertyChanged("NewPassword");
            }
        }
    }
}