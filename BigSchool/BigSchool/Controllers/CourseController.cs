using BigSchool.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigSchool.Controllers
{
    public class CourseController : Controller
    {
        public ActionResult Create()
        {
            Model1 context = new Model1();
            Course objCourse = new Course();
            objCourse.ListCategory = context.Categories.ToList();
            return View(objCourse);
        }
        // GET: Course
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course objCourse)
        {
            Model1 context = new Model1();
            ModelState.Remove("LectunerId");
            if (!ModelState.IsValid)
            {
                objCourse.ListCategory = context.Categories.ToList();
                return View("Creat", objCourse);
            }

            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            objCourse.LectunerId = user.Id;

            context.Courses.Add(objCourse);
            context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
        public ActionResult Attending()
        {
            Model1 context = new Model1();
            ApplicationUser currentUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().
                FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var listAttendances = context.Attendances.Where(x => x.Attendee == currentUser.Id).ToList();
            var courses = new List<Course>();
            foreach (Attendance temp in listAttendances)
            {
                Course objCourse = temp.Course;
                objCourse.LectureName = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().
                    FindById(objCourse.LectunerId).Name;
                courses.Add(objCourse);
            }
            return View(courses);
        }

        public ActionResult Mine()
        {
            Model1 context = new Model1();
            ApplicationUser currentUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().
                FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var courses = context.Courses.Where(c => c.LectunerId == currentUser.Id && c.DateTimea > DateTime.Now).ToList();
            foreach (Course i in courses)
            {
                i.LectureName = currentUser.Name;
            }

            return View(courses);
        }
        public ActionResult Edit(int id)
        {
            Model1 context = new Model1();
            Course course = context.Courses.FirstOrDefault(x => x.Id == id);
            course.ListCategory = context.Categories.ToList();
            return View(course);
        }

        [Authorize]
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Course objCourse)
        {
            Model1 context = new Model1();
            context.Courses.AddOrUpdate(objCourse);
            context.SaveChanges();
            objCourse.ListCategory = context.Categories.ToList();
            return View(objCourse);
        }
        public ActionResult Delete(int id)
        {
            Model1 context = new Model1();
            Course course = context.Courses.FirstOrDefault(x => x.Id == id);
            if (course != null)
            {
                return View(course);
            }
            return HttpNotFound();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteItem(int id)
        {
            Model1 context = new Model1();
            Course course = context.Courses.FirstOrDefault(x => x.Id == id);
            if (course != null)
            {
                context.Courses.Remove(course);
                context.SaveChanges();
                return RedirectToAction("Mine");
            }
            return HttpNotFound();
        }
    }
}
