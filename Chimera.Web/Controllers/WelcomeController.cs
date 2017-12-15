using System.Web.Mvc;

namespace Chimera.Web.Controllers
{
    public class WelcomeController : Controller
    {
        //
        // GET: /Welcome/

        public ActionResult Index()
        {
            return View();
        }

    }
}
