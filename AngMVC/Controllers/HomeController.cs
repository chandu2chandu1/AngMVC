using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AngMVC.Models;
using System.Web.Mvc;

namespace AngMVC.Controllers
{
    public class HomeController : Controller
    {
        SchoolModel schoolContext = new SchoolModel();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult GetSchools()
        {
           
            var results = schoolContext.Schools.ToList();
            return Json(results, JsonRequestBehavior.AllowGet); 
        }

        [HttpPost]
        public ActionResult UpdateSchool(School school)
        {
            School selectedSchool = schoolContext.Schools.First(s => s.ID == school.ID);
            if (selectedSchool == null)
                return new HttpNotFoundResult();
            selectedSchool.SchoolName = school.SchoolName;
            selectedSchool.City = school.City;

            schoolContext.SaveChanges();
            return Json(school, JsonRequestBehavior.DenyGet);
        }
    }
}
