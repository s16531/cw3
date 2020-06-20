using System;
using Microsoft.AspNetCore.Mvc;
using ABD_cw3.Models;
using ABD_cw3.DTOs.Requests;
using ABD_cw3.DTOs.Responses;
using System.Data.SqlClient;
using ABD_cw3.Services;
using Microsoft.AspNetCore.Authorization;

namespace ABD_cw3.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    [Authorize(Roles="employee")]
    public class EnrollmentsController : ControllerBase
    {
        public EnrollmentsController()
        {
        }
        private readonly IStudentsDbService _dbService;
        public EnrollmentsController(IStudentsDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost]
        public IActionResult Enroll([FromBody] EnrollStudentRequest request)
        {
            Enrollment enr = _dbService.EnrollStudent(request);
            if (enr != null) return Ok();
            else return BadRequest();
        }

        [HttpPost("promotions")]
        public IActionResult PromoteStudents(PromoteStudentRequest request)
        {
            Enrollment result = _dbService.PromoteStudents(request.Semester, request.Studies);
            if (result == null) return NotFound();
            return Created("", result);
        }
    }
}