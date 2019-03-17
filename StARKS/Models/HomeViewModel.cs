using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace StARKS.Models
{
    public class HomeViewModel
    {
        public List<SCM> AllSCM { get; set; }
        public List<Student> Students { get; set; }
        public List<Course> Courses { get; set; }


        
        public int Id { get; set; }



        public int ?Sid { get; set; }


        [MaxLength(256)]
        public string FirstName { get; set; }

        [MaxLength(256)]
        public string LastName { get; set; }

        [MaxLength(256)]
        public string Adress { get; set; }

        [MaxLength(256)]
        public string City { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; }





        public int ?Cid { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(256)]
        public string Description { get; set; }

    }
}
