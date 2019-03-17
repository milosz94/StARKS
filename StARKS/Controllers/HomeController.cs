using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StARKS.Data;
using StARKS.Models;

namespace StARKS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Update(int? sid, int? cid, int? val)
        {
            if (val < 6 || val > 10)
                return RedirectToAction("Index");

            var toUpdate = from m in _context.Marks
                           where m.StudentId == sid && m.CourseId == cid
                           select new Mark
                           {
                               Id = m.Id,
                               Student = m.Student,
                               StudentId = m.StudentId,
                               Course = m.Course,
                               CourseId = m.CourseId

                           };


            if (toUpdate.FirstOrDefault() == null)
            {
                Mark m = new Mark
                {
                    CourseId = cid,
                    StudentId = sid,
                    Grade = val

                };
                _context.Marks.Add(m);
                _context.SaveChangesAsync();
            }
            else
            {
                toUpdate.First().Grade = val;
                Mark m1 = new Mark
                {
                    Id = toUpdate.First().Id,
                    CourseId = toUpdate.First().CourseId,
                    StudentId = toUpdate.First().StudentId,
                    Student = toUpdate.First().Student,
                    Course = toUpdate.First().Course,
                    Grade = val
                };


                _context.Marks.Update(m1);
                _context.SaveChangesAsync();
            }



            return PartialView();
            //return PartialView("<h3>" + sid.ToString() + " Sadameee! changed to" + val.ToString() + " " + cid.ToString() + " </h3>", "text/html");
        }
        
        public IActionResult Index()
        {

            ModelState.Clear();
            
           

               var info =
                      (from st in _context.Students.Include("Marks")
                       from co in _context.Courses.Include("Marks")
                       select new
                       {
                           sid = st.Id,
                           sname = st.FirstName + " " + st.LastName,
                           cid = co.Id,
                           cname = co.Name,
                           grad = st.Marks.Where(o => o.StudentId == st.Id && o.CourseId == co.Id
                               ).FirstOrDefault().Grade
                       }

                       ).ToList();
              var  studList = _context.Students.ToList();
              var coursList = _context.Courses.ToList();
            
            

            HomeViewModel hvm = new HomeViewModel
            {
                AllSCM = new List<SCM>(),

                Students = studList,

                Courses = coursList
            };


            for (int i = 0; i < studList.Count; i++)
            {
                SCM sc = new SCM();
                sc.Sid = studList[i].Id;
                sc.FirstName = studList[i].FirstName;
                sc.Sname = studList[i].FirstName + " " + studList[i].LastName;

                var tmpL = new List<Mark>();
                foreach (var inf in info)
                {

                    if (inf.sid == studList[i].Id)
                    {
                        tmpL.Add(new Mark { Grade = inf.grad, CourseId = inf.cid });
                        //tmpL.Add(inf.grad,inf.cid);
                    }
                }
                sc.Grades = tmpL;
                hvm.AllSCM.Add(sc);


            }
           return View(hvm);
        }


        [HttpPost]
        public IActionResult Index(HomeViewModel hvm)
        {
            return View(hvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Adress,City,DateOfBirth,Gender")] HomeViewModel hvm)
        {
            if (ModelState.IsValid)
            {
                Student student = new Student { Id = hvm.Id, FirstName = hvm.FirstName, LastName = hvm.LastName, Adress = hvm.Adress, City = hvm.City, DateOfBirth = hvm.DateOfBirth, Gender = hvm.Gender };


                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateC([Bind("Cid,Name,Description")] HomeViewModel hvm)
        {
            if (ModelState.IsValid)
            {
                Course course = new Course {Id = hvm.Cid ?? 0, Name=hvm.Name , Description= hvm.Description };
                _context.Courses.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStudent(int id, [Bind("Sid,FirstName,LastName,Adress,City,DateOfBirth,Gender")] HomeViewModel hvm)
        {
            if (ModelState.IsValid)
            {
                
                Student student = new Student { Id =id, FirstName = hvm.FirstName, LastName = hvm.LastName, Adress = hvm.Adress, City = hvm.City, DateOfBirth = hvm.DateOfBirth, Gender = hvm.Gender };

                _context.Update(student);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
              return View("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCourse(int id, [Bind("Id,Name,Description")] HomeViewModel hvm)
        {
            if (ModelState.IsValid)
            {

                Course course = new Course { Id = hvm.Id, Name = hvm.Name, Description = hvm.Description };
                _context.Update(course);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            return View("Index");
        }







        public IActionResult SelectStudent(int? id)
        {
            var stud = _context.Students.Find(id);

            var info =
                    (from st in _context.Students.Include("Marks")
                     from co in _context.Courses.Include("Marks")
                     select new
                     {
                         sid = st.Id,
                         sname = st.FirstName + " " + st.LastName,
                         cid = co.Id,
                         cname = co.Name,
                         grad = st.Marks.Where(o => o.StudentId == st.Id && o.CourseId == co.Id
                             ).FirstOrDefault().Grade
                     }

                     ).ToList();
            var studList = _context.Students.ToList();
            var coursList = _context.Courses.ToList();



            HomeViewModel hvm = new HomeViewModel
            {
                AllSCM = new List<SCM>(),

                Students = studList,

                Courses = coursList
            };


            for (int i = 0; i < studList.Count; i++)
            {
                SCM sc = new SCM();
                sc.Sid = studList[i].Id;
                sc.Sname = studList[i].FirstName + " " + studList[i].LastName;

                var tmpL = new List<Mark>();
                foreach (var inf in info)
                {

                    if (inf.sid == studList[i].Id)
                    {
                        tmpL.Add(new Mark { Grade = inf.grad, CourseId = inf.cid });
                        //tmpL.Add(inf.grad,inf.cid);
                    }
                }
                sc.Grades = tmpL;
                hvm.AllSCM.Add(sc);


            }

            hvm.Sid = id;
            hvm.Id = id ?? 0;
            hvm.FirstName = stud.FirstName;
            hvm.LastName = stud.LastName;
            hvm.DateOfBirth = stud.DateOfBirth;
            hvm.Gender = stud.Gender;
            hvm.Adress = stud.Adress;
            hvm.City = stud.City;

            
            return View("Index",hvm);
        }



        public IActionResult SelectCourse(int ?id)
        {
            var courses = _context.Courses.Find(id);

            var info =
                    (from st in _context.Students.Include("Marks")
                     from co in _context.Courses.Include("Marks")
                     select new
                     {
                         sid = st.Id,
                         sname = st.FirstName + " " + st.LastName,
                         cid = co.Id,
                         cname = co.Name,
                         grad = st.Marks.Where(o => o.StudentId == st.Id && o.CourseId == co.Id
                             ).FirstOrDefault().Grade
                     }

                     ).ToList();
            var studList = _context.Students.ToList();
            var coursList = _context.Courses.ToList();



            HomeViewModel hvm = new HomeViewModel
            {
                AllSCM = new List<SCM>(),

                Students = studList,

                Courses = coursList
            };


            for (int i = 0; i < studList.Count; i++)
            {
                SCM sc = new SCM();
                sc.Sid = studList[i].Id;
                sc.Sname = studList[i].FirstName + " " + studList[i].LastName;

                var tmpL = new List<Mark>();
                foreach (var inf in info)
                {

                    if (inf.sid == studList[i].Id)
                    {
                        tmpL.Add(new Mark { Grade = inf.grad, CourseId = inf.cid });
                        //tmpL.Add(inf.grad,inf.cid);
                    }
                }
                sc.Grades = tmpL;
                hvm.AllSCM.Add(sc);


            }
            hvm.Cid = id;
            hvm.Description = courses.Description;
            hvm.Name = courses.Name;


            return View("Index", hvm);
        }




        public IActionResult DeleteStudent(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var student = _context.Students.Find(id);



            _context.Students.Remove(student);
            var mark = from m in _context.Marks
                       where m.StudentId == student.Id
                       select m;

            _context.Marks.RemoveRange(mark);

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteCourse(int? cid)
        {
            if (cid == null)
            {
                return NotFound();
            }
            var course = _context.Courses.Find(cid);

            _context.Courses.Remove(course);
            var mark = from m in _context.Marks
                       where m.CourseId == course.Id
                       select m;
            _context.Marks.RemoveRange(mark);

            _context.SaveChanges();
            return RedirectToAction("Index");

        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
