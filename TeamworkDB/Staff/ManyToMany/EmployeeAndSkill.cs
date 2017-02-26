using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamworkDB
{
    public class EmployeeAndSkill
    {
        private int employee_Id;
        private int skill_Id;
        private int skillProficiency_Id;


        public EmployeeAndSkill() { }

        public EmployeeAndSkill(int employee_Id, int skill_Id,
            int skillProficiency_Id)
        {
            this.employee_Id = employee_Id;
            this.skill_Id = skill_Id;
            this.skillProficiency_Id = skillProficiency_Id;
        }


        public int Employee_Id
        {
            get { return employee_Id; }
            set { employee_Id = value; }
        }

        public int Skill_Id
        {
            get { return skill_Id; }
            set { skill_Id = value; }
        }

        public int SkillProficiency_Id
        {
            get { return skillProficiency_Id; }
            set { skillProficiency_Id = value; }
        }

        public virtual Employee Employee { get; set; }
        public virtual Skill Skill { get; set; }
        public virtual SkillProficiency SkillProficiency { get; set; }
    }
}
