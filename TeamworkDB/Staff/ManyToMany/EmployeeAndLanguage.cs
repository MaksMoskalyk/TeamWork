using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamworkDB
{
    public class EmployeeAndLanguage
    {
        private int employee_Id;
        private int language_Id;
        private int languageProficiency_Id;


        public EmployeeAndLanguage() { }

        public EmployeeAndLanguage(int employee_Id, int language_Id,
            int languageProficiency_Id)
        {
            this.employee_Id = employee_Id;
            this.language_Id = language_Id;
            this.languageProficiency_Id = languageProficiency_Id;
        }


        public int Employee_Id
        {
            get { return employee_Id; }
            set { employee_Id = value; }
        }

        public int Language_Id
        {
            get { return language_Id; }
            set { language_Id = value; }
        }

        public int LanguageProficiency_Id
        {
            get { return languageProficiency_Id; }
            set { languageProficiency_Id = value; }
        }

        public virtual Employee Employee { get; set; }
        public virtual Language Language { get; set; }
        public virtual LanguageProficiency LanguageProficiency { get; set; }
    }
}
