using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StARKS.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public List<Mark> Marks { get; set; }

        [MaxLength(256)]
        public string firstname { get; set; }

        [MaxLength(256)]
        public string lastname { get; set; }

        [MaxLength(256)]
        public string adress { get; set; }

        [MaxLength(256)]
        public string city { get; set; }

        public DateTime dateofbirth { get; set; }

        public string gender { get; set; }

        

    }
}
