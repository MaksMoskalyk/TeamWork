using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamworkDB
{
    public class CompanyCreator : ElementCreator
    {
        public override IElement CreateCategory()
        {
            Company company = new Company();
            return company;
        }
    }
}
