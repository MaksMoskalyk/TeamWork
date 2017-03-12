using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffDatabaseUnit
{
    public class AddEmployeeViewFactory : AbstractViewFactory
    {
        public override IView CreateView()
        {
            return new AddEmployeeView();
        }
    }
}
