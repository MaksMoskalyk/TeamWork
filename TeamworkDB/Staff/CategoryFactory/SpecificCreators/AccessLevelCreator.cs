using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamworkDB
{
    public class AccessLevelCreator : ElementCreator
    {
        public override IElement CreateCategory()
        {
            AccessLevel accessLevel = new AccessLevel();
            return accessLevel;
        }
    }
}
