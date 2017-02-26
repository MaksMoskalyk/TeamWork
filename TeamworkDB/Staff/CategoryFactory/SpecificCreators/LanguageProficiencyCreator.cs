using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamworkDB
{
    public class LanguageProficiencyCreator : ElementCreator
    {
        public override IElement CreateCategory()
        {
            LanguageProficiency languageProficiency = new LanguageProficiency();
            return languageProficiency;
        }
    }
}
