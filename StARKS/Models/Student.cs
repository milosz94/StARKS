using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StARKS.Models
{
    public partial class Student
    {
        public Student()
        {
            this.Marks = new HashSet<Mark>();

        }


        [Key]
        public int Id { get; set; }
        

        [MaxLength(256)]
        public string FirstName { get; set; }

        [MaxLength(256)]
        public string LastName { get; set; }

        [MaxLength(256)]
        public string Adress { get; set; }

        [MaxLength(256)]
        public string City { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; }

        
        public virtual ICollection<Mark> Marks { get; set; }
    }
}
