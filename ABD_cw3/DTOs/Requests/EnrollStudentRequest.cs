using System;
using System.ComponentModel.DataAnnotations;

namespace ABD_cw3.DTOs.Requests
{
    public class EnrollStudentRequest
    {
        [Required]
        public int IdStudent { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string IndexNumber { get; set; }
        [Required]
        public string Studies { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
    }

}
