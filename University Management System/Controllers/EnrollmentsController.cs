using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityManagement.Core.Servicces;
using UniversityManagement.Core.ViewModels;

namespace University_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService EnrollmentService;

        public EnrollmentsController(IEnrollmentService enrollmentService)
        {
            EnrollmentService = enrollmentService;
        }
        // GET: api/<EnrollmentsController>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var enrollments = await EnrollmentService.GetAllAsync();
            return Ok(enrollments);
        }

        // GET api/<EnrollmentsController>/5/5
        [HttpGet("{courseId}/{studentId}")]
        public async Task<IActionResult> GetByCourseAndStudentAsync(int courseId, int studentId)
        {
            var response = await EnrollmentService.GetByCourseAndStudentAsync(courseId, studentId);
            if (response.Data == null)
                return StatusCode(response.StatusCode, response);

            return Ok(response);
        }

        // POST api/<EnrollmentsController>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddAsync(EnrollmentVM enrollment)
        {
            var response = await EnrollmentService.AddAsync(enrollment);
            return StatusCode(response.StatusCode, response);
        }

        // DELETE api/<EnrollmentsController>/5/5
        [HttpDelete("{courseId}/{studentId}")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(int courseId, int studentId)
        {
            var response = await EnrollmentService.DeleteAsync(courseId, studentId);
            return StatusCode(response.StatusCode, response);
        }
    }
}
