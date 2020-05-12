using System;
using Microsoft.AspNetCore.Mvc;
using ABD_cw3.Models;
using ABD_cw3.DTOs.Requests;
using ABD_cw3.DTOs.Responses;
using System.Data.SqlClient;

namespace ABD_cw3.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        public EnrollmentsController()
        {
        }
        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest enrollment)
        {
            using var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s16531;Integrated Security=True");
            using (var com = new SqlCommand())
            {
                com.Connection = con;

                con.Open();
                var tran = con.BeginTransaction();
                com.CommandText = "select IdStudies from studies where name=@name";
                com.Parameters.AddWithValue("name", enrollment.Studies);

                var dr = com.ExecuteReader();
                if (!dr.Read())
                {
                    tran.Rollback();
                    return BadRequest("Nie ma studiów");
                }
                int idStudies = (int)dr["IdStudy"];

                com.CommandText = "select IdEnrollments from Enrollments where Semester = 1 and " +
                    "IdStudy=@idStudy";
                com.Parameters.AddWithValue("idStudy", idStudies);

                dr = com.ExecuteReader();
                if(!dr.Read())
                {
                    com.CommandText = "INSERT INTO ENROLLMENTS(IdEnrollment,Semester,IdStudy,StartDate) VALUES(@IdEnrollment,1,@IdStudy,@Now)";
                    com.Parameters.AddWithValue("Now", DateTime.Now);
                    com.Parameters.AddWithValue("IdEnrollment", 5000);
                    com.Parameters.AddWithValue("IdStudy", idStudies);
                    dr = com.ExecuteReader();
                }

                com.CommandText = "select * from Students where IndexNumber = @IndexNumber";
                com.Parameters.AddWithValue("IndexNumber", enrollment.IndexNumber);
                dr = com.ExecuteReader();

                if (!dr.Read())
                {
                    com.CommandText = "INSERT INTO STUDENT(IndexNumber,FirstName,LastName,BirthDate,IdEnrollment) VALUES" +
                        "(@IndexNumber,@FirstName,@LastName,@BirthDate,@IdEnrollment)";
                    com.Parameters.AddWithValue("IndexNumber", enrollment.IndexNumber);
                    com.Parameters.AddWithValue("IndexNumber", enrollment.FirstName);
                    com.Parameters.AddWithValue("IndexNumber", enrollment.LastName);
                    com.Parameters.AddWithValue("IndexNumber", enrollment.BirthDate);
                    com.Parameters.AddWithValue("IndexNumber", enrollment.IndexNumber);
                    dr = com.ExecuteReader();
                }
                tran.Commit();
            }
            var response = new EnrollStudentResponse();
            response.IndexNumber = enrollment.IndexNumber;
            response.FirstName = enrollment.FirstName;
            response.LastName = enrollment.LastName;
            response.BirthDate = enrollment.BirthDate;
            response.IndexNumber = enrollment.IndexNumber;

            return Ok(response);
        }

        [Route("promotions")]
        [HttpPost]
        public IActionResult PromoteStudents()
        {
            return Ok();
        }
    }
}