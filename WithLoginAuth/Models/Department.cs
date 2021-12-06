using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WithLoginAuth.Models
{
    public class Department
    {

        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Department ID")]
        public int DepId { get; set; }
        [Required]
        [Display(Name = "Department Name")]
        public string DepName { get; set; }

        public virtual List<Student> Students { get; set; }
    }
}