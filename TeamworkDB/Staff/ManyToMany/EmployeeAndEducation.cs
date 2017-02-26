using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamworkDB
{
    public class EmployeeAndEducation
    {
        private int employee_Id;
        private int educationFacility_Id;
        private int educationSpecialty_Id;
        private int educationEntitlingDocument_Id;
        private DateTime graduationDate;


        public EmployeeAndEducation() { }

        public EmployeeAndEducation(int employee_Id, int educationFacility_Id,
            int educationSpecialty_Id, int educationEntitlingDocument_Id, DateTime graduationDate)
        {
            this.employee_Id = employee_Id;
            this.educationFacility_Id = educationFacility_Id;
            this.educationSpecialty_Id = educationSpecialty_Id;
            this.educationEntitlingDocument_Id = educationEntitlingDocument_Id;
            this.graduationDate = graduationDate;
        }


        public int Employee_Id
        {
            get { return employee_Id; }
            set { employee_Id = value; }
        }

        public int EducationFacility_Id
        {
            get { return educationFacility_Id; }
            set { educationFacility_Id = value; }
        }

        public int EducationSpecialty_Id
        {
            get { return educationSpecialty_Id; }
            set { educationSpecialty_Id = value; }
        }

        public int EducationEntitlingDocument_Id
        {
            get { return educationEntitlingDocument_Id; }
            set { educationEntitlingDocument_Id = value; }
        }

        public DateTime GraduationDate
        {
            get { return graduationDate; }
            set { graduationDate = value; }
        }

        public virtual Employee Employee { get; set; }
        public virtual EducationFacility EducationFacility { get; set; }
        public virtual EducationSpecialty EducationSpecialty { get; set; }
        public virtual EducationEntitlingDocument EducationEntitlingDocument { get; set; }
    }
}
