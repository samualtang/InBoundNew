using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace WCFMainBeltInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ServiceHost host = new ServiceHost(typeof(MainBeltService));
                if (host.State != CommunicationState.Opened)
                {
                    host.Opened += (s, e) => Console.Write("服务器开启成功....");
                    host.Open();
                    Console.Read();
                  
                }
            }
            catch (Exception ex)
            {

                Console.Write("" + ex.Message);
                Console.Read();
            }

        }
    }
}
