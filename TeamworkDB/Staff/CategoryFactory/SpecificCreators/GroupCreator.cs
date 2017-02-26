using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamworkDB
{
    public class GroupCreator : ElementCreator
    {
        public override IElement CreateCategory()
        {
            SkillsGroup group = new SkillsGroup();
            return group;
        }
    }
}
