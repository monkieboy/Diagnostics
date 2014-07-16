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
            return View();
        }

        public ActionResult Fields()
        {
            return View();
        }

    }

    public class ViewingModel
    {
        public string Status { get; set; }
    }
}
