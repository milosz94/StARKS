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
        
        public IActionResult Index(string filterS, string filterC)
        {

            ModelState.Clear();

            dynamic info;
            dynamic studList;
            dynamic coursList;
            if (filterS == null && filterC == null)
            {
                info =
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
                studList = _context.Students.ToList();
                coursList = _context.Courses.ToList();
            }

            else if (filterS != null && filterC == null)
            {
                info =
                       (from st in _context.Students.Include("Marks")
                        from co in _context.Courses.Include("Marks")
                        where st.FirstName.StartsWith(filterS)
                        select new
                        {
                            sid = st.Id,
                            sname = st.FirstName + " " + st.LastName,
                            cid = co.Id,
                            cname = co.Name,
                            grad = st.Marks.Where(o => o.StudentId == st.Id && o.CourseId == co.Id).FirstOrDefault().Grade
                        }).ToList();

                studList = (from st in _context.Students
                            where st.FirstName.StartsWith(filterS)
                            select st).ToList();
                coursList = _context.Courses.ToList();

            }
            else if (filterS == null && filterC != null)
            {
                info =
                        (from st in _context.Students.Include("Marks")
                         from co in _context.Courses.Include("Marks")
                         where co.Name.StartsWith(filterC)
                         select new
                         {
                             sid = st.Id,
                             sname = st.FirstName + " " + st.LastName,
                             cid = co.Id,
                             cname = co.Name,
                             grad = st.Marks.Where(o => o.StudentId == st.Id && o.CourseId == co.Id
                                 ).FirstOrDefault().Grade
                         }).ToList();

                studList = _context.Students.ToList();

                coursList = (from co in _context.Courses
                             where co.Name.StartsWith(filterC)
                             select co
                ).ToList();
            }
            else
            {
                info =
                       (from st in _context.Students.Include("Marks")
                        from co in _context.Courses.Include("Marks")
                        where st.FirstName.StartsWith(filterS) && co.Name.StartsWith(filterC)
                        select new
                        {
                            sid = st.Id,
                            sname = st.FirstName + " " + st.LastName,
                            cid = co.Id,
                            cname = co.Name,
                            grad = st.Marks.Where(o => o.StudentId == st.Id && o.CourseId == co.Id
                                ).FirstOrDefault().Grade
                        }).ToList();

                studList = (from st in _context.Students
                            where st.FirstName.StartsWith(filterS)
                            select st

                               ).ToList();

                coursList = (from co in _context.Courses
                             where co.Name.StartsWith(filterC)
                             select co
                                ).ToList();
            }



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
            if(HttpContext.Request.Method == HttpMethod.Post.Method)
                 return RedirectToAction("Index");
            else
                 return View(new HomeViewModel { AllSCM = hvm.AllSCM , Courses=hvm.Courses , Students= hvm.Students });
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
                Course course = new Course {Id = hvm.Cid, Name=hvm.Name , Description= hvm.Description };
                _context.Courses.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View("Index");
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
public IActionResult Error()
{
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
    }
}
