using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamworkDB;

namespace StaffDatabaseUnit
{
    public class WebServiceTableUnit
    {
        private WebService service;
        private string account;

        public WebServiceTableUnit()
        {
            service = new WebService();
            account = "";
        }

        public WebServiceTableUnit(WebService service, string account)
        {
            this.service = service;
            this.account = account;
        }

        public WebService Service
        {
            get { return service; }
            set { service = value; }
        }

        public string Account
        {
            get { return account; }
            set { account = value; }
        }
    }
}
