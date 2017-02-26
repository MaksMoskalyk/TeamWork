using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamworkDB;

namespace StaffDatabaseUnit
{
    public class SkillsInfo
    {
        private Skill skill;
        private SkillProficiency proficiency;

        public SkillsInfo()
        {
            skill = new Skill();
            proficiency = new SkillProficiency();
        }

        public Skill Skill
        {
            get { return skill; }
            set { skill = value; }
        }

        public SkillProficiency Proficiency
        {
            get { return proficiency; }
            set { proficiency = value; }
        }
    }
}
