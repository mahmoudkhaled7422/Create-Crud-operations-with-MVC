using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace WithLoginAuth.Models
{
    public class Course
    {
        [Key]
        public int courseId { get; set; }
        [Required]
        public string CourseName { get; set; }
        public virtual List<Student> Students { get; set; }

    }
}