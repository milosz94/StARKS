using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace StARKS.Models
{
    public class HomeViewModel
    {
        public List<SCM> AllSCM { get; set; }
        public List<Student> Students { get; set; }
        public List<Course> Courses { get; set; }

    }
}
