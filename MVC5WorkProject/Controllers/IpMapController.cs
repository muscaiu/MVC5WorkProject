
using MVC5WorkProject.Models;
using System.Web.Mvc;

namespace MVC5WorkProject.Controllers
{
    public class IpMapController : Controller
    {
        private IpMapContext db = new IpMapContext();

        // GET: IpMap
        //[Authorize]
        public ActionResult IpMap()
        {
            return View(db.IpMapList);
        }
    }
}