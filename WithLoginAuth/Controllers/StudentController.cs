using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WithLoginAuth.Models;

namespace WithLoginAuth.Controllers
{
    [Authorize(Roles = "Student , Admin")]
    public class StudentController : Controller 
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Student
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.Department);
            return View(students.ToList());
        }

        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepId", "DepName");
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                //var allCourses = db.StudentCourses.ToList();
                //var CoureseIn = db.DepartmentCourses.Where(a => a.DeptId == student.DepartmentId).Select(a=>a.CourseId).ToList();
                //var c = from dc in db.DepartmentCourses where dc.DeptId==student.DepartmentId
                //      join  s in db.Courses 
                //        on dc.CourseId equals s.courseId
                //        select  new

                //        {
                //           dc.CourseId,
                //           student.Id
                //      }
                //        ; 
                //var std = student.Id;
                //db.StudentCourses.Add(c);
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepId", "DepName", student.DepartmentId);
            return View(student);
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepId", "DepName", student.DepartmentId);
            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepId", "DepName", student.DepartmentId);
            return View(student);
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CoursesList(int id)
        {
            var stdCourses = db.StudentCourses.Where(a=>a.StudentID==id).ToList();
            ViewBag.std = db.Students.FirstOrDefault(a => a.Id==id);
            return View(stdCourses);
        }
        public ActionResult AddCourse(int id)
        {
            var allCourses = db.Courses.ToList();
            var CourseIn = db.StudentCourses.Where(a => a.StudentID == id).Select(a => a.Course);
            var CourseNotIn = allCourses.Except(CourseIn).ToList();
            ViewBag.std = db.Students.FirstOrDefault(a => a.Id == id);
            return View(CourseNotIn);
        }
        [HttpPost]
        public ActionResult AddCourse(int id, Dictionary<string, bool> crs)
        {
            foreach (KeyValuePair<string, bool> item in crs)
            {
                if (item.Value == true)
                {
                    db.StudentCourses.Add(new StudentCourse() { StudentID = id, CourseId = int.Parse(item.Key) });
                }
            }
            db.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult RemoveCourse(int id)
        {
            var CoureseIn = db.StudentCourses.Where(a => a.StudentID == id).Select(a => a.Course).ToList();
            ViewBag.std = db.Students.FirstOrDefault(a => a.Id == id);
            return View(CoureseIn);
        }
        [HttpPost]
        public ActionResult RemoveCourse(int id, Dictionary<string, bool> crs)
        {
            foreach (var item in crs)
            {
                if (item.Value == true)
                {
                    int key = int.Parse(item.Key);
                    var courseIn = db.StudentCourses.FirstOrDefault(a => a.StudentID == id && a.CourseId == key);
                    db.StudentCourses.Remove(courseIn);
                }
            }
            db.SaveChanges();
            return RedirectToAction("index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
