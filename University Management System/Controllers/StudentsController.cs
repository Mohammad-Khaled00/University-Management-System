using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityManagement.Core.Servicces;
using UniversityManagement.Core.ViewModels;

namespace University_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService StudentService;

        public StudentsController(IStudentService studentService)
        {
            StudentService = studentService ?? throw new ArgumentNullException(nameof(studentService));
        }

        // GET: api/<StudentsController>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var students = await StudentService.GetAllAsync();
            return Ok(students);
        }

        // GET api/<StudentsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await StudentService.GetByIdAsync(id);
            if (response.Data == null)
                return StatusCode(response.StatusCode, response);

            return Ok(response);
        }

        // POST api/<StudentsController>
        [HttpPost]
        public async Task<IActionResult> AddAsync(StudentVM student)
        {
            var response = await StudentService.AddAsync(student);
            return StatusCode(response.StatusCode, response);
        }

        // PUT api/<StudentsController>/5
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateAsync(StudentVM student, [FromHeader(Name = "Authorization")] string authorizationHeader)
        {
            string jwtToken = authorizationHeader?.Replace("Bearer ", "");
            var response = await StudentService.UpdateAsync(student, jwtToken);
            return StatusCode(response.StatusCode, response);
        }

        // DELETE api/<StudentsController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(int id, [FromHeader(Name = "Authorization")] string authorizationHeader)
        {
            string jwtToken = authorizationHeader?.Replace("Bearer ", "");
            var response = await StudentService.DeleteAsync(id, jwtToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}
