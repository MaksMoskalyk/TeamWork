using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamworkDB
{
    public class EducationSpecialtyCreator : ElementCreator
    {
        public override IElement CreateCategory()
        {
            EducationSpecialty specialty = new EducationSpecialty();
            return specialty;
        }
    }
}
