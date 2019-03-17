using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StARKS.Models
{
    public class SCM
    {
        public int Sid { get; set; }
        public string FirstName { get; set; }
        public string Sname { get; set; }
        public List<Mark> Grades { get; set; }

    }
}
