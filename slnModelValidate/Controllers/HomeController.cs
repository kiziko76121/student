using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using slnModelValidate.Models;
using System.Data;
using System.Data.SqlClient;

namespace slnModelValidate.Controllers
{
    public class HomeController : Controller
    {
        dbStudentEntities db = new dbStudentEntities();
        string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;" +
            "AttachDbFilename=|DataDirectory|dbStudent.mdf;" +
            "Integrated Security=True";
        private void excuteSql(string sql)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString =constr;
            con.Open();
            SqlCommand cmd = new SqlCommand(sql,con);
            try {
                cmd.ExecuteNonQuery();
            } catch (Exception e)
            {
                e.ToString();
            };
            con.Close();
        }
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
             (string fStuId, string fName, string fEmail, int fScore, tStudent student)
         {
            if (ModelState.IsValid)
            {

                string sql = "Update tStudent " +
                    "set fName=N'"+ fName + "',"+
                    "fEmail='" + fEmail + "'," +
                    "fScore=" + fScore + "" +
                    " where fStuId='" + fStuId + "'"
                    ;
                excuteSql(sql);
                return RedirectToAction("Index");
            }
            return View(student);
            
            
         }

         
        

    }
}