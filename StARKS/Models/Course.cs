using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StARKS.Models
{
    public class Course
    {
        [Key]
        public int code { get; set; }
        public List<Mark> Marks { get; set; }

        [MaxLength(100)]
        public string name { get; set; }

        [MaxLength(256)]
        public string description { get; set; }

        
        //public ICollection<Student> Students { get; set; }
    }
}
