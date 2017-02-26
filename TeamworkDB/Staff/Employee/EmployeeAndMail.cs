using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamworkDB
{
    public class EmployeeAndMail
    {
        private int employee_Id;
        private string mail;


        public EmployeeAndMail() { }

        public EmployeeAndMail(int employee_Id, string mail)
        {
            this.employee_Id = employee_Id;
            this.mail = mail;
        }


        public int Employee_Id
        {
            get { return employee_Id; }
            set { employee_Id = value; }
        }

        public string Mail
        {
            get { return mail; }
            set { mail = value; }
        }

        public virtual Employee Employee { get; set; }
    }
}
