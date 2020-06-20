using System;
using ABD_cw3.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using ABD_cw3.Models;
using Microsoft.Extensions.Configuration;
using System.Text;
using ABD_cw3.DTOs.Requests;
using ABD_cw3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace ABD_cw3.Controllers
{
    [ApiController]
    [Route("api/students")]

    public class StudentsController : ControllerBase
    {
        private readonly IStudentsDbService _dbService;
        private readonly IConfiguration _configuration;

        public StudentsController(IStudentsDbService dbService, IConfiguration configuration)
        {
            _dbService = dbService;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetStudents(string orderBy)
        {
            var listOfStudents = new List<Student>();
            using var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s16531;Integrated Security=True");
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select FirstName,Lastname,BirthDate,name,Semester from Student stu" +
                                  "inner join Enrollment e on e.IdEnrollment = stu.IdEnrollment" +
                                  "inner join Studies s on s.IdStudy = e.IdStudy";
                con.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var st = new Student();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.IndexNumber = dr["IndexNumber"].ToString();
                    listOfStudents.Add(st);
                }
            }
            return Ok(listOfStudents);
        }

        [HttpGet]
        public IActionResult GetEnrolments(string indexNumber)
        {
            var listOfStudents = new List<Student>();
            using var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s16531;Integrated Security=True");
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from Enrollment E where E.IdEnrollment in (select IdEnrollment from Student where IndexNumber = @indexNumber";
                con.Open();
                var dr = com.ExecuteReader();
                var en = new String("");
                while (dr.Read())
                {
                   
                    en += dr["IdEnrollment"].ToString();
                    en += dr["Semester"].ToString() + " ";
                    en += dr["IdStudy"].ToString() + " ";
                    en += dr["StartDate"].ToString();
                }
                return Ok(en);
            }
    
        }

        [HttpGet("{indexNumber}")]
        public IActionResult getStudent(string indexNumber)
        {
            using var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s16531;Integrated Security=True");
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from Student where IndexNumber = @indexNumber";
                SqlParameter parameter = new SqlParameter();
                parameter.Value = indexNumber;
                parameter.ParameterName = "index";
                com.Parameters.Add(parameter);

                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    var st = new Student();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.IndexNumber = dr["IndexNumber"].ToString();
                    return Ok(st);
                }

                return NotFound();
            }


        }


        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            if (id == 2)
            {
                return Ok("Kowalski");
            }
            else
            {
                return NotFound("Nie znaleziono");
            }
            
        }

        [HttpPost]
        public IActionResult CreateStudent([FromBody]Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }

        [HttpPut("{id}")]
        public IActionResult PutStudent(int id)
        {
            return Ok("CHANGED");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            return Ok("DELTED");
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginRequest request)
        {
            var claims = _dbService.Login(request);
            return Authorize(claims);
        }

        [HttpPost("refresh-token/{token}")]
        public IActionResult Login(string token)
        {
            var claims = _dbService.Login(token);
            return Authorize(claims);
        }

        private IActionResult Authorize(AuthenticationService result)
        {
            if (result == null)
            {
                return Unauthorized();
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "Gakko",
                audience: "Students",
                claims: result.Claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
            );
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = result.RefreshToken
            });

        }
    }
}
