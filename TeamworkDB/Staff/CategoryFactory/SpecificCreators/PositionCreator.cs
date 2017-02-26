using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamworkDB
{
    public class PositionCreator : ElementCreator
    {
        public override IElement CreateCategory()
        {
            Position position = new Position();
            return position;
        }
    }
}
