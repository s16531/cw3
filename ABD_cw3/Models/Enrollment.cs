using System;
using System.ComponentModel.DataAnnotations;

namespace ABD_cw3.Models
{
    public class Enrollment
    {

        [Required]
        public int IdEnrollment { get; set; }
        [Required]
        public int Semester { get; set; }
        [Required]
        public int IdStudy { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
    }
}
