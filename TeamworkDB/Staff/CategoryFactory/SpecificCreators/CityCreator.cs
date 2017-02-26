using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamworkDB
{
    public class CityCreator : ElementCreator
    {
        public override IElement CreateCategory()
        {
            City city = new City();
            return city;
        }
    }
}
