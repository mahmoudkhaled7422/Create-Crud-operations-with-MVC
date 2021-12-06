using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WithLoginAuth.Models
{
    public class StudentCourse
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Student")]
        public int StudentID { get; set; }
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public int Degree { get; set; }
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
    }
}