using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamworkDB
{
    public class Skill
    {
        private int id;
        private string name;
        private int skillsGroup_Id;
        

        public Skill() { }

        public Skill(string name, int skillsGroup_Id)
        {
            this.name = name;
            this.skillsGroup_Id = skillsGroup_Id;           
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

        public int SkillsGroup_Id
        {
            get { return skillsGroup_Id; }
            set { skillsGroup_Id = value; }
        }
     
        public virtual SkillsGroup SkillsGroup { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<EmployeeAndSkill> EmployeesAndSkills { get; set; }
    }
}
