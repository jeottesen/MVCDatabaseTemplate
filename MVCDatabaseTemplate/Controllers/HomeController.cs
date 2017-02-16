using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BestPracticeTemplate.Models;

namespace BestPracticeTemplate.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<StudentCourse> studentClasses = new List<StudentCourse>();
            studentClasses = OracleMethods.getCourses();
            return View();
        }

        public ActionResult OracleCourses()
        {
            List<StudentCourse> studentClasses = new List<StudentCourse>();
            studentClasses = OracleMethods.getCourses();
            return View(studentClasses);
        }

        public ActionResult OracleDetails(int id)
        {
            StudentCourse course = OracleMethods.getCourse(id);
            return View(course);
        }

        public ActionResult OracleEdit(int id)
        {
            StudentCourse course = OracleMethods.getCourse(id);
            return View(course);
        }

        [HttpPost]
        public ActionResult OracleEdit(StudentCourse course)
        {
            StudentCourse studentCourse = new StudentCourse();
            if (ModelState.IsValid)
            {
                studentCourse = OracleMethods.updateCourse(course);
            }
            return View("OracleDetails", studentCourse);
        }

/************************* MS Access Section *********************************/

        public ActionResult MSAccessCourses()
        {
            List<StudentCourse> studentCourses = new List<StudentCourse>();
            studentCourses = MSAccessMethods.getCourses();
            return View(studentCourses);
        }

        public ActionResult MSAccessDetails(int id)
        {
            StudentCourse course = new StudentCourse();
            course = MSAccessMethods.getCourse(id);
            return View(course);
        }

        public ActionResult MSAccessEdit(int id)
        {
            StudentCourse course = new StudentCourse();
            course = MSAccessMethods.getCourse(id);
            return View(course);
        }

        [HttpPost]
        public ActionResult MSAccessEdit(StudentCourse course)
        {
            StudentCourse studentCourse = new StudentCourse();
            if (ModelState.IsValid)
            {
                studentCourse = MSAccessMethods.updateCourse(course);
            }
            return View("MSAccessDetails", studentCourse);
        }


        /************************ MS SQL Server Section *******************************/
        public ActionResult MSSQLCourses()
        {
            List<StudentCourse> studentCourses = new List<StudentCourse>();
            studentCourses = MSSQLMethods.getCourses();
            return View(studentCourses);
        }

        public ActionResult MSSQLDetails(int id)
        {
            StudentCourse studentCourse = new StudentCourse();
            studentCourse = MSSQLMethods.getCourse(id);
            return View(studentCourse);
        }

        public ActionResult MSSQLEdit(int id)
        {
            StudentCourse studentCourse = new StudentCourse();
            studentCourse = MSSQLMethods.getCourse(id);
            return View(studentCourse);
        }

        [HttpPost]
        public ActionResult MSSQLEdit(StudentCourse course)
        {
            StudentCourse studentCourse = new StudentCourse();
            if (ModelState.IsValid)
            {
                //studentCourse = MSSQLMethods.updateCourse(course);
                studentCourse = MSSQLMethods.updateCourseSQL(course);
            }
            return View("MSSQLDetails",studentCourse);
        }

        public ActionResult MSSQLDelete(int id)
        {

            
            return RedirectToAction("MSSQLCourses");
        }
    }
}