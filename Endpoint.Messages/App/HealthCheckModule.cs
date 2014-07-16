using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;
using Nancy.Routing;
using Newtonsoft.Json;

namespace Endpoint.Messages.App
{
    public class HealthCheckModule : NancyModule
    {
        public HealthCheckModule(IRouteCacheProvider routeCacheProvider)
        {
            Get["/"] = x =>
                {
                    var list = new List<string>();
                    foreach (var module in routeCacheProvider.GetCache())
                    {
                        list.AddRange(module.Value.Select(route => route.Item2.Path.Replace("/", string.Empty)));
                    }
                    
                    return JsonConvert.SerializeObject(list, Formatting.Indented);
                };

            Get["/random"] = r =>
            {
                var state = new Random().Next() % 7 == 0;

                var response = JsonConvert.SerializeObject(new HealthCheckStatus
                {
                    Name = "Random",
                    Color = state ? "green" : "red",
                    State = state
                }, Formatting.Indented);

                return response;
            };
            
            Get["/blab"] = r =>
            {
                var state = new Random().Next() % 2 == 0;

                var response = JsonConvert.SerializeObject(new HealthCheckStatus
                {
                    Name = "Blab",
                    Color = state ? "green" : "red",
                    State = state
                }, Formatting.Indented);

                return response;
            };
            
            Get["/localhost"] = r =>
            {
                var response = JsonConvert.SerializeObject(new HealthCheckStatus
                {
                    Name = "Blab",
                    Color = "green",
                    State = true
                }, Formatting.Indented);

                return response;
            };
        }
    }
}
