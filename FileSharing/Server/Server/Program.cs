using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace FileSharing.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost serviceHost = new ServiceHost(typeof(Service));

            serviceHost.Open();
            Console.WriteLine("Press <ENTER> to stop service\n");
            Console.ReadLine();
            serviceHost.Close();
        }
    }
}
