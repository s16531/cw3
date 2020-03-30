using System;
using ABD_cw3.DAL;
using ABD_cw3.Models;
using Microsoft.AspNetCore.Mvc;


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
            return Ok(_dbService.GetStudents());
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
