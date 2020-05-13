using System;
using System.Data.SqlClient;
using ABD_cw3.DTOs.Requests;
using ABD_cw3.Models;

namespace ABD_cw3.Services
{
    public class SqlServerDbService : IStudentsDbService
    {
        public SqlServerDbService()
        {
        }
        public Student EnrollStudent(EnrollStudentRequest enrollment)
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
                    return null;
                }
                int idStudies = (int)dr["IdStudy"];

                com.CommandText = "select IdEnrollments from Enrollments where Semester = 1 and " +
                    "IdStudy=@idStudy";
                com.Parameters.AddWithValue("idStudy", idStudies);

                dr = com.ExecuteReader();
                if (!dr.Read())
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
            Student student  = new Student();
            student.IndexNumber = enrollment.IndexNumber;
            student.FirstName = enrollment.FirstName;
            student.LastName = enrollment.LastName;
            student.BirthDate = enrollment.BirthDate;

            return student;
        }

        public Enrollment PromoteStudents(PromoteStudentRequest promoteStudentRequest)
        {
            using var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s16531;Integrated Security=True");
            using (var com = new SqlCommand())
            {
                com.Connection = con;

                con.Open();
                com.CommandText = "EXEC PromoteStudents2 @Studies = @requestStudies ,@Semester = @requestSemester";
                com.Parameters.AddWithValue("requestStudies", promoteStudentRequest.Studies);
                com.Parameters.AddWithValue("requestSemester", promoteStudentRequest.Semester);
                var dr = com.ExecuteReader();
                dr.Close();
                dr = com.ExecuteReader();
                Enrollment enr = new Enrollment();
                enr.IdEnrollment = (int)dr["IdEnrollment"];
                enr.Semester = (int)(dr["Semester"]);
                enr.IdStudy = (int)(dr["IdStudy"]);
                enr.StartDate = (DateTime)(dr["StartDate"]);
                return enr;
            }
        }

        public bool CheckIndexNumber(string index)
        {
            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s16531;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                //Tutaj sie zatrzymuje
                con.Open();
                com.CommandText = "select * from Student where IndexNumber=@index";
                com.Parameters.AddWithValue("index", index);
                var dr = com.ExecuteReader();
                if (dr.Read())
                {
                    return true;
                }
                else return false;
            }
        }

        public Enrollment PromoteStudents(int semester, string studies)
        {
            throw new NotImplementedException();
        }

        Enrollment IStudentsDbService.EnrollStudent(EnrollStudentRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
