using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffDatabaseUnit
{
    class EditEmployeeDataViewFactory : AbstractViewFactory
    {
        public override IView CreateView()
        {
            return new EditEmployeeDataView();
        }
    }
}
