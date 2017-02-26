using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamworkDB
{
    public class EducationEntitlingDocumentCreator : ElementCreator
    {
        public override IElement CreateCategory()
        {
            EducationEntitlingDocument entitlingDocument = 
                new EducationEntitlingDocument();
            return entitlingDocument;
        }
    }
}
