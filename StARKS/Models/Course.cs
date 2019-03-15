using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StARKS.Models
{
    public partial class Course
    {
        [Key]
        public int Id { get; set; }
       

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(256)]
        public string Description { get; set; }

        public virtual ICollection<Mark> Marks { get; set; }
    }
}
