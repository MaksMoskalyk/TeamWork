using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamworkDB
{
    public class EmployeeAndWeb
    {
        private int employee_Id;
        private int webService_Id;
        private string account;


        public EmployeeAndWeb() { }

        public EmployeeAndWeb(int employee_Id, int webService_Id,
            string account)
        {
            this.employee_Id = employee_Id;
            this.webService_Id = webService_Id;
            this.account = account;
        }


        public int Employee_Id
        {
            get { return employee_Id; }
            set { employee_Id = value; }
        }

        public int WebService_Id
        {
            get { return webService_Id; }
            set { webService_Id = value; }
        }

        public string Account
        {
            get { return account; }
            set { account = value; }
        }

        public virtual Employee Employee { get; set; }
        public virtual WebService WebService { get; set; }
    }
}
