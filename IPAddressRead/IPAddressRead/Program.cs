using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IPAddressRead
{
    class Program
    {
        static void Main(string[] args)
        {
            var domainList = new[] { "www.google.com", "www.microsoft.com", "docs.microsoft.com", "stackoverflow.com" };

            foreach (var domain in domainList)
            {
                var ipAddress = Dns.GetHostAddresses(domain);
                Console.WriteLine(ipAddress[0]);

                IPEndPoint remoteEP = null;
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create($"https://{domain}");
                req.ServicePoint.BindIPEndPointDelegate = delegate (ServicePoint servicePoint, IPEndPoint remoteEndPoint, int retryCount) {
                    remoteEP = remoteEndPoint;
                    return null;
                };
                req.GetResponse();
                Console.WriteLine(remoteEP.Address.ToString());
            }
            Console.ReadLine();
        }
    }
}
