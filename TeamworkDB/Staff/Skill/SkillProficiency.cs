using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamworkDB
{
    public class SkillProficiency : IElement
    {
        private int id;
        private string name;


        public SkillProficiency() { }

        public SkillProficiency(string name)
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

        public virtual ICollection<EmployeeAndSkill> EmployeesAndSkills { get; set; }
    }
}
