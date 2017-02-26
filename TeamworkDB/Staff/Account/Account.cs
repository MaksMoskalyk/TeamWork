using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamworkDB
{
    public class Account
    {
        private int employee_Id;       
        private string login;
        private string password;
        private int accessLevel_Id;


        public Account() { }

        public Account(int employee_Id, string login, string password,
            int accessLevel_Id)
        {
            this.employee_Id = employee_Id;
            this.login = login;
            this.password = password;
            this.accessLevel_Id = accessLevel_Id;
        }


        public int Employee_Id
        {
            get { return employee_Id; }
            set { employee_Id = value; }
        }

        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public int AccessLevel_Id
        {
            get { return accessLevel_Id; }
            set { accessLevel_Id = value; }
        }

        public virtual Employee Employee { get; set; }
        public virtual AccessLevel AccessLevel { get; set; }      
    }
}
