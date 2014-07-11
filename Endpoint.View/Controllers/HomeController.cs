using System.IO;
using System.Net;
using System.Web.Mvc;

namespace Endpoint.View.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var req = WebRequest.Create("http://localhost:41100/random");

            var status = string.Empty;
            using (var response = req.GetResponse())
            {
                using (var rdr = new StreamReader(response.GetResponseStream()))
                {
                    status = rdr.ReadToEnd();
                }
            }


            var model = new ViewingModel
                {
                    Status = status
                };
            return View(model);
        }

    }

    public class ViewingModel
    {
        public string Status { get; set; }
    }
}
