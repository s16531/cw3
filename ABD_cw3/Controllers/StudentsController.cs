using System;
using ABD_cw3.DAL;
using ABD_cw3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ABD_cw3.Controllers
{
    [ApiController]
    [Route("api/students")]

    public class StudentsController : ControllerBase
    {
        private readonly IDbService _dbService;

        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
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
    }
}
