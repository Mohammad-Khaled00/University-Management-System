using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityManagement.Core.Servicces;
using UniversityManagement.Core.ViewModels;

namespace University_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService CourseService;

        public CoursesController(ICourseService courseService)
        {
            CourseService = courseService;
        }

        // GET: api/<CoursesController>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var courses = await CourseService.GetAllAsync();
            return Ok(courses);
        }

        // GET api/<CoursesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await CourseService.GetByIdAsync(id);
            if (response.Data == null)
                return StatusCode(response.StatusCode, response);

            return Ok(response);
        }

        // POST api/<CoursesController>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddAsync(CourseVM course, [FromHeader(Name = "Authorization")] string authorizationHeader)
        {
            string jwtToken = authorizationHeader?.Replace("Bearer ", "");
            var response = await CourseService.AddAsync(course, jwtToken);
            return StatusCode(response.StatusCode, response);
        }

        // PUT api/<CoursesController>/5
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateAsync(CourseVM course, [FromHeader(Name = "Authorization")] string authorizationHeader)
        {
            string jwtToken = authorizationHeader?.Replace("Bearer ", "");
            var response = await CourseService.UpdateAsync(course, jwtToken);
            return StatusCode(response.StatusCode, response);
        }

        // DELETE api/<CoursesController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(int id, [FromHeader(Name = "Authorization")] string authorizationHeader)
        {
            string jwtToken = authorizationHeader?.Replace("Bearer ", "");
            var response = await CourseService.DeleteAsync(id, jwtToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}
