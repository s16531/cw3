using System;
using System.ComponentModel.DataAnnotations;

namespace ABD_cw3.DTOs.Responses
{
    public class PromoteStudentResponse
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
