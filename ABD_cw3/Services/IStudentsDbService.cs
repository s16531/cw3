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
        public AuthenticationService Login(LoginRequest request);
        public AuthenticationService Login(string request);
    }
}
