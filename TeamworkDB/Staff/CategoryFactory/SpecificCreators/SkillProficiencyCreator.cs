using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamworkDB
{
    public class SkillProficiencyCreator : ElementCreator
    {
        public override IElement CreateCategory()
        {
            SkillProficiency skillProficiency = new SkillProficiency();
            return skillProficiency;
        }
    }
}
