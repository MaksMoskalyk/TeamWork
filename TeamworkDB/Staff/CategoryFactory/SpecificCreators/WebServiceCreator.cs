using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamworkDB
{
    public class WebServiceCreator : ElementCreator
    {
        public override IElement CreateCategory()
        {
            WebService webService = new WebService();
            return webService;
        }
    }
}
