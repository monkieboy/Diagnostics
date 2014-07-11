using System;
using Nancy;

namespace Endpoint.Messages
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/random"] = r =>
                {
                    return new Random().Next()%7 == 0 ? "red" : "green";
                };
        }
    }
}
