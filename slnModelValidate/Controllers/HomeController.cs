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

        public ActionResult Delete(string id)
        {
            var student = db.tStudent.
                Where(m=>m.fStuId==id).FirstOrDefault();
            db.tStudent.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Edit(string id)
        {
            var student = db.tStudent.
                Where(m => m.fStuId == id).FirstOrDefault();
            return View(student);
        }

         [HttpPost]
         public ActionResult Edit
             (string id, string name, string email, string score)
         {
            if (score == null)
            {
                score = "0";
            }
            tStudent student = db.tStudent.
                 Where(m => m.fStuId == id).FirstOrDefault();
             student.fName = name;
             student.fEmail = email;
            Console.WriteLine(score);

            student.fScore = int.Parse(score);
            try {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                e.ToString();
            }
             
             return RedirectToAction("Index");
         }
         
        

    }
}