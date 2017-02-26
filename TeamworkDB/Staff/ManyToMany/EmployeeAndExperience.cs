using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamworkDB
{
    public class EmployeeAndExperience
    {
        private int employee_Id;
        private int position_Id;
        private int company_Id;
        private DateTime hiringDate;
        private DateTime dismissalDate;
        

        public EmployeeAndExperience() { }

        public EmployeeAndExperience(int employee_Id, int position_Id, int company_Id,
            DateTime hiringDate, DateTime dismissalDate)
        {
            this.employee_Id = employee_Id;
            this.position_Id = position_Id;
            this.company_Id = company_Id;
            this.hiringDate = hiringDate;
            this.dismissalDate = dismissalDate;
        }


        public int Employee_Id
        {
            get { return employee_Id; }
            set { employee_Id = value; }
        }

        public int Position_Id
        {
            get { return position_Id; }
            set { position_Id = value; }
        }

        public int Company_Id
        {
            get { return company_Id; }
            set { company_Id = value; }
        }

        public DateTime HiringDate
        {
            get { return hiringDate; }
            set { hiringDate = value; }
        }

        public DateTime DismissalDate
        {
            get { return dismissalDate; }
            set { dismissalDate = value; }
        }
     
        public virtual Employee Employee { get; set; }
        public virtual Position Position { get; set; }
        public virtual Company Company { get; set; }
    }
}
