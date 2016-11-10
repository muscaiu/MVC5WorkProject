using System.Web.Mvc;

namespace MVC5WorkProject.Controllers
{
    public class CalendarController : Controller
    {
        // GET: Calendar
       // [Authorize]
        public ActionResult Calendar()
        {
            return View();
        }
    }
}