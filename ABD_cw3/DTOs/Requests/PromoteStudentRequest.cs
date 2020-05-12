using System;
using System.ComponentModel.DataAnnotations;

namespace ABD_cw3.DTOs.Requests
{
    public class PromoteStudentRequest
    {
        [Required]
        public string Studies { get; set; }
        [Required]
        public int Semester { get; set; }
    }
}
