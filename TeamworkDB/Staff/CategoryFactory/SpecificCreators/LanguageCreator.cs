using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamworkDB
{
    public class LanguageCreator : ElementCreator
    {
        public override IElement CreateCategory()
        {
            Language language = new Language();
            return language;
        }
    }
}
