using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamworkDB
{
    public class EmployeeAndPhone
    {
        private int employee_Id;
        private string phoneNumber;


        public EmployeeAndPhone() { }

        public EmployeeAndPhone(int employee_Id, string phoneNumber)
        {
            this.employee_Id = employee_Id;
            this.phoneNumber = phoneNumber;
        }


        public int Employee_Id
        {
            get { return employee_Id; }
            set { employee_Id = value; }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        public virtual Employee Employee { get; set; }
    }
}
