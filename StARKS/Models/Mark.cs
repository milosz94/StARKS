﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StARKS.Models
{
    public partial class Mark
    {
        //select s.id ,s.firstname, s.lastname,c.code, c.name, 1 from Students s ,Courses c
        [Key]
        public int Id { get; set; }

        public int? StudentId { get; set; }
        public Student Student { get; set; }

        public int? CourseId { get; set; }
        public Course Course { get; set; }
       
        public int? Grade { get; set; }

        //public List<Student> Students { get; set; }
        //public  List<Course> Courses { get; set; }

    }
}
