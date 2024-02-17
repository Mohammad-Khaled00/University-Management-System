using AutoMapper;
using UniversityManagement.Core.Models;
using UniversityManagement.Core.Repositories;
using UniversityManagement.Core.Servicces;
using UniversityManagement.Core.ViewModels;

namespace UniversityManagement.app.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepo _enrollmentRepo;
        private readonly IMapper _mapper;

        public EnrollmentService(IEnrollmentRepo enrollmentRepo, IMapper mapper)
        {
            _enrollmentRepo = enrollmentRepo ?? throw new ArgumentNullException(nameof(enrollmentRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ResponseEntity<IEnumerable<EnrollmentVM>>> GetAllAsync()
        {
            var response = new ResponseEntity<IEnumerable<EnrollmentVM>>();
            try
            {
                var enrollments = await _enrollmentRepo.GetAllAsync();
                response.Data = _mapper.Map<IEnumerable<EnrollmentVM>>(enrollments);
                response.StatusCode = 200; // OK
            }
            catch (Exception ex)
            {
                response.Errors = new List<string> { ex.Message };
                response.StatusCode = 500; // Internal Server Error
            }
            return response;
        }

        public async Task<ResponseEntity<EnrollmentVM>> GetByCourseAndStudentAsync(int courseId, int studentId)
        {
            var response = new ResponseEntity<EnrollmentVM>();
            try
            {
                var enrollment = await _enrollmentRepo.GetByCourseAndStudentAsync(courseId, studentId);
                if (enrollment == null)
                {
                    response.Errors = new List<string> { "Enrollment not found" };
                    response.StatusCode = 404; // Not Found
                }
                else
                {
                    response.Data = _mapper.Map<EnrollmentVM>(enrollment);
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

        public async Task<ResponseEntity<EnrollmentVM>> AddAsync(EnrollmentVM enrollment, string jwtToken)
        {
            var user = JwtTokenHelper.ExtractClaimsFromToken(jwtToken);
            var response = new ResponseEntity<EnrollmentVM>();
            try
            {
                enrollment.CreatedBy = user;
                enrollment.IsDeleted = false;
                var enrollmentModel = _mapper.Map<Enrollment>(enrollment);
                await _enrollmentRepo.AddAsync(enrollmentModel);
                response.Data = enrollment;
                response.StatusCode = 200; // OK
            }
            catch (Exception ex)
            {
                response.Errors = new List<string> { ex.Message };
                response.StatusCode = 500; // Internal Server Error
            }
            return response;
        }

        public async Task<ResponseEntity<bool>> DeleteAsync(int courseId, int studentId, string jwtToken)
        {
            var user = JwtTokenHelper.ExtractClaimsFromToken(jwtToken);
            var response = new ResponseEntity<bool>();
            try
            {
                await _enrollmentRepo.DeleteAsync(courseId, studentId, user);
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
