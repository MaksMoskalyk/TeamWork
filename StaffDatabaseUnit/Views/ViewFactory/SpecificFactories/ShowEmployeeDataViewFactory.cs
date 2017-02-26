using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffDatabaseUnit
{
    public class ShowEmployeeDataViewFactory : AbstractViewFactory
    {
        public override IView CreateView()
        {
            return new ShowEmployeeDataView();
        }
    }
}
