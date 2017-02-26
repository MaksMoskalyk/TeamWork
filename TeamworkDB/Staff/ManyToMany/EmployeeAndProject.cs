using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text;
using System.Threading.Tasks;

namespace TeamworkDB
{
    public class EmployeeAndProject
    {
        private int employee_Id;
        private int project_Id;
        private bool isFavorite;


        public EmployeeAndProject() { }

        public EmployeeAndProject(int employee_Id, int project_Id)
        {
            this.employee_Id = employee_Id;
            this.project_Id = project_Id;
        }


        public int Employee_Id
        {
            get { return employee_Id; }
            set { employee_Id = value; }
        }

        public int Project_Id
        {
            get { return project_Id; }
            set { project_Id = value; }
        }

        public bool IsFavorite
        {
            get { return isFavorite; }
            set { isFavorite = value; }
        }


        public virtual Employee Employee { get; set; }
        public virtual Project Project { get; set; }
    }
}
