using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamworkDB
{
    public class LanguageProficiency : IElement
    {
        private int id;
        private string name;


        public LanguageProficiency() { }

        public LanguageProficiency(string name)
        {
            this.name = name;
        }


        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual ICollection<EmployeeAndLanguage> EmployeesAndLanguages { get; set; }
    }
}
