using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using slnModelValidate.Models;

namespace slnModelValidate.Controllers
{
    public class HomeController : Controller
    {
        dbStudentEntities db = new dbStudentEntities();

        public ActionResult Index()
        {
            var standents = db.tStudent.ToList();
            return View(standents);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(tStudent student)
        {
            if (ModelState.IsValid)
            {
                db.tStudent.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        public ActionResult Delete(String id)
        {
            var student = db.tStudent.
                Where(m=>m.fStuId==id).FirstOrDefault();
            db.tStudent.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
            public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}