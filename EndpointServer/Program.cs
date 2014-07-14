using System;
using Endpoint.Messages;
using Nancy.Hosting.Self;

namespace EndpointServer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new NancyHost(new Uri("http://localhost:41100")))
            {
                host.Start();
                Console.ReadKey();
            }
        }
    }
}
