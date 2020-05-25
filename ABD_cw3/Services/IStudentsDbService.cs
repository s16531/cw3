using System;
using System.Security.Claims;
using ABD_cw3.DTOs.Requests;
using ABD_cw3.Models;

namespace ABD_cw3.Services
{
    public interface IStudentsDbService
    {
        Enrollment EnrollStudent(EnrollStudentRequest request);
        Enrollment PromoteStudents(int semester, string studies);
        bool CheckIndexNumber(string index);
        public AuthenticationResult Login(LoginRequestDto request);
        public AuthenticationResult Login(string request);
    }
}
