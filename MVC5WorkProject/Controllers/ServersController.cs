using System.Threading;
using System.Web;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using MVC5WorkProject.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MVC5WorkProject.Controllers
{
    public class ServersController : Controller
    {
        private ServersContext db = new ServersContext();
        

        // GET: Servers/Index
        public ActionResult Index()
        {
            //var servers = db.ServerList.Include("Servers");
            //return View("Index", servers.ToList());

            //var serversList = db.ServerList.ToList();

            //var model = new ServersVM();
            //using (var dbo = new ServersContext())
            //{
            //    model.Servers = dbo.ServerList.ToList();
            //}

            return View();
        }
        //POST: Servers/Index
        public ActionResult IndexVM()
        {
            var model = new ServersVM();
            using (var dbo = new ServersContext())
            {
                model.Servers = dbo.ServerList.ToList();
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // GET: Servers/Edit/5
        public ActionResult Edit(int? id)
        {
            Thread.Sleep(2000);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servers servers = db.ServerList.Find(id);
            if (servers == null)
            {
                return HttpNotFound();
            }
            return View(servers);
        }
        // POST: Servers/Edit
        [HttpPost]
        public ActionResult Edit(Servers serverModel)
        {
            if (ModelState.IsValid)
            {
                using (var dbo = new ServersContext())
                {
                    var server = new Servers()
                    {
                        EnumStatus = serverModel.EnumStatus,
                        EnumType = serverModel.EnumType,
                        Name = serverModel.Name,
                        Password = serverModel.Password,
                        Ip1 = serverModel.Ip1,
                        Ip2 = serverModel.Ip2,
                        Ip3 = serverModel.Ip3,
                        Details = serverModel.Details
                    };
                    dbo.ServerList.Add(server);
                    dbo.SaveChanges();

                    return Json(server, JsonRequestBehavior.AllowGet);
                }
            }
            throw new HttpException(400, "eroare in model");
        }
        // POST: Servers/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ServerId,EnumStatus,EnumType,Name,Password,Ip1,Ip2,Ip3,Details")] Servers servers)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(servers).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return Json(servers, JsonRequestBehavior.AllowGet);
        //    }
        //    throw new HttpException(400, "eroare in model");
        //}

        // GET: Servers/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Servers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServerId,EnumStatus,EnumType,Name,Password,Ip1,Ip2,Ip3,Details")] Servers servers)
        {
            //dynamic validation from Controller
            if (string.IsNullOrEmpty(servers.Name))
            {
                ModelState.AddModelError("Name", "Ai uitat Numele!");
            }

            if (ModelState.IsValid)
            {
                db.ServerList.Add(servers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(servers);
        }
        
        // GET: Servers/Delete/5
        public ActionResult Delete(int id)
        {
            using (var dbo = new ServersContext())
            {
                var item = dbo.ServerList.FirstOrDefault(x => x.ServerId == id);
                if (item != null)
                {
                    dbo.ServerList.Remove(item);
                    dbo.SaveChanges();
                }
            }     
            return RedirectToAction("Index");
        }

        //Delete with Get 
       //WHAt is contacts?
        //public ActionResult Delete(int id)
        //{
        //    var contacts = db.Contacts.Find(id);
        //    var details = db.ContactTelefons.Find(id);
        //    db.Contacts.Remove(contacts);
        //    if (details != null)
        //    {
        //        db.ContactTelefons.Remove(details);
        //    }
        //    db.SaveChanges();

        //    return RedirectToAction("Index");
        //}


        // POST: Servers/Delete/5
        //[HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Servers servers = db.ServerList.Find(id);
        //    db.ServerList.Remove(servers);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        // GET: Servers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Servers servers = db.ServerList.Find(id);

            if (servers == null)
            {
                return HttpNotFound();
            }
            return View(servers);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



        //TwoColumns
        //public ActionResult TwoColumns()
        //{
        //    var servers = db.ServerList.Include("Name")
        //        .GroupBy(x => x.Name)
        //        .Select(y => new TwoColumns
        //        {
        //            Name = y.Key,
        //            Total = y.Count()
        //        }).ToList()
        //        .OrderByDescending(y => y.Total);

        //    return View();
        //}
    }
}
