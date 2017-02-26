using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamworkDB
{
    public abstract class ElementCreator
    {
        abstract public IElement CreateCategory();
    }
}
