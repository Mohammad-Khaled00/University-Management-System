using AutoMapper;
using UniversityManagement.Core.Models;
using UniversityManagement.Core.Repositories;
using UniversityManagement.Core.Servicces;
using UniversityManagement.Core.ViewModels;

namespace UniversityManagement.app.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepo _courseRepo;
        private readonly IMapper _mapper;

        public CourseService(ICourseRepo courseRepo, IMapper mapper)
        {
            _courseRepo = courseRepo ?? throw new ArgumentNullException(nameof(courseRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ResponseEntity<IEnumerable<CourseVM>>> GetAllAsync()
        {
            var response = new ResponseEntity<IEnumerable<CourseVM>>();
            try
            {
                var courses = await _courseRepo.GetAllAsync();
                response.Data = _mapper.Map<IEnumerable<CourseVM>>(courses);
                response.StatusCode = 200; // OK
            }
            catch (Exception ex)
            {
                response.Errors = new List<string> { ex.Message };
                response.StatusCode = 500; // Internal Server Error
            }
            return response;
        }

        public async Task<ResponseEntity<CourseVM>> GetByIdAsync(int id)
        {
            var response = new ResponseEntity<CourseVM>();
            try
            {
                var course = await _courseRepo.GetByIdAsync(id);
                if (course == null)
                {
                    response.Errors = new List<string> { "Course not found" };
                    response.StatusCode = 404 ; // Not Found
                }
                else
                {
                    response.Data = _mapper.Map<CourseVM>(course);
                    response.StatusCode = 200; // OK
                }
            }
            catch (Exception ex)
            {
                response.Errors = new List<string> { ex.Message };
                response.StatusCode = 500; // Internal Server Error
            }
            return response;
        }

        public async Task<ResponseEntity<CourseVM>> AddAsync(CourseVM course, string jwtToken)
        {
            var user = JwtTokenHelper.ExtractClaimsFromToken(jwtToken);
            var response = new ResponseEntity<CourseVM>();
            try
            {
                course.CreatedBy = user;
                course.CreatedDate = DateTime.Now;
                course.IsDeleted = false;
                var courseModel = _mapper.Map<Course>(course);
                await _courseRepo.AddAsync(courseModel);
                response.Data = course;
                response.StatusCode = 200; // OK
            }
            catch (Exception ex)
            {
                response.Errors = new List<string> { ex.Message };
                response.StatusCode = 500; // Internal Server Error
            }
            return response;
        }

        public async Task<ResponseEntity<CourseVM>> UpdateAsync(CourseVM course, string jwtToken)
        {
            var user = JwtTokenHelper.ExtractClaimsFromToken(jwtToken);
            var response = new ResponseEntity<CourseVM>();
            try
            {
                course.ModifiedBy = user;
                var courseModel = _mapper.Map<Course>(course);
                await _courseRepo.UpdateAsync(courseModel);
                response.Data = course;
                response.StatusCode = 200; // OK
            }
            catch (Exception ex)
            {
                response.Errors = new List<string> { ex.Message };
                response.StatusCode = 500; // Internal Server Error
            }
            return response;
        }

        public async Task<ResponseEntity<bool>> DeleteAsync(int id, string jwtToken)
        {
            var user = JwtTokenHelper.ExtractClaimsFromToken(jwtToken);
            var response = new ResponseEntity<bool>();
            try
            {
                await _courseRepo.DeleteAsync(id, user);
                response.Data = true;
                response.StatusCode = 200; // OK
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.Errors = new List<string> { ex.Message };
                response.StatusCode = 500; // Internal Server Error
            }
            return response;
        }
    }
}
