using System;
using System.Collections.Generic;
using Nancy;
using Nancy.Routing;
using Newtonsoft.Json;
using System.Linq;

namespace Endpoint.Messages
{
    public class HomeModule : NancyModule
    {
        public HomeModule(IRouteCacheProvider routeCacheProvider)
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
        }
    }

    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void RequestStartup(Nancy.TinyIoc.TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines, NancyContext context)
        {
            pipelines.AfterRequest.AddItemToEndOfPipeline(ctx => ctx.Response
                .WithHeader("Access-Control-Allow-Origin", "*")
                .WithHeader("Access-Control-Allow-Methods", "POST,GET")
                .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, X-Requested-With, Content-type")
                .ContentType = "application/json");
        }
    }

    public static class NancyExtensions
    {
        public static void EnableCors(this NancyModule module)
        {
            module.After.AddItemToEndOfPipeline(x => x.Response.WithHeader("Access-Control-Allow-Origin", "*"));
        }
    }


    public class HealthCheckStatus
    {
        public string Color
        {
            get;
            set;
        }

        public bool State
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }
}
