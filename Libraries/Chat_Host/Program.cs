using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using FileSharing.Server;
using ProgramMessenger_WcfSL;


namespace Service_Host
{
    class Program
    {
        static void Main(string[] args)
        {

            ServiceHost fileHost = new ServiceHost(typeof(Service));
            fileHost.Open();

            ServiceHost PMHost = new ServiceHost(typeof(ServiceProgramMessenger));
            PMHost.Open();

            foreach (ServiceEndpoint se in fileHost.Description.Endpoints)
                Console.WriteLine(se.Address.ToString());

            foreach (ServiceEndpoint se in PMHost.Description.Endpoints)
                Console.WriteLine(se.Address.ToString());

            Console.ReadLine();
            fileHost.Close();
            PMHost.Close();
        }
    }
}
