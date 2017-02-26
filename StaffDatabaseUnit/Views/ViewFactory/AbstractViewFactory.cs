using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffDatabaseUnit
{
    public abstract class AbstractViewFactory
    {
        abstract public IView CreateView();
    }
}
