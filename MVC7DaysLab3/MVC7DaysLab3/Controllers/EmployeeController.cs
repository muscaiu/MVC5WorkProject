using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using MVC7DaysLab3.Models;

namespace MVC7DaysLab3.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            var emp = new Employee();
            emp.FirstName = "Cris";
            emp.LastName = "Mus";
            emp.Salary = 2000;
            ViewData["Employee"] = emp;

            return View();
        }
    }
}