using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamworkDB
{
    public class CountryCreator : ElementCreator
    {
        public override IElement CreateCategory()
        {
            Country country = new Country();
            return country;
        }
    }
}
