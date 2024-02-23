using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityManagement.Core.Servicces;
using UniversityManagement.Core.ViewModels;

namespace University_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorsController : ControllerBase
    {
        private readonly IInstructorService InstructorService;

        public InstructorsController(IInstructorService instructorService)
        {
            InstructorService = instructorService;
        }

        // GET: api/<InstructorsController>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var instructors = await InstructorService.GetAllAsync();
            return Ok(instructors);
        }

        // GET api/<InstructorsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await InstructorService.GetByIdAsync(id);
            if (response.Data == null)
                return StatusCode(response.StatusCode, response);

            return Ok(response);
        }

        // POST api/<InstructorsController>
        [HttpPost]
        public async Task<IActionResult> AddAsync(INSRegestrationVM instructor)
        {
            var response = await InstructorService.Register(instructor);
            return StatusCode(response.StatusCode, response);
        }

        // PUT api/<InstructorsController>/5
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateAsync(InstructorVM instructor)
        {
            var response = await InstructorService.UpdateAsync(instructor);
            return StatusCode(response.StatusCode, response);
        }

        // DELETE api/<InstructorsController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await InstructorService.DeleteAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
