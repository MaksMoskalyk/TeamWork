using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamworkDB
{
    public class EducationFacilityCreator : ElementCreator
    {
        public override IElement CreateCategory()
        {
            EducationFacility facility = new EducationFacility();
            return facility;
        }
    }
}
