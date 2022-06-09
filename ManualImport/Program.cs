using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ManualImport
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            var importService = new RawImport_Lightspeed.ImportLightspeed();
            DateTime? sDate = new DateTime(2020, 07, 25);
            List<string> Dealers = new List<string> { "PIV1023" };
            List<string> NoFilter = new List<string>();
            do
            {
                var wait = importService.ServiceWork(NoFilter);// Dealers, sDate);
                wait++;
                if (wait < 0) wait = 1;
                Thread.Sleep(wait);
            } while (true);

        }
    }
}
