using AutoMapper;
using UniversityManagement.Core.Models;
using UniversityManagement.Core.Repositories;
using UniversityManagement.Core.Servicces;
using UniversityManagement.Core.ViewModels;

namespace UniversityManagement.app.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepo _studentRepo;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepo studentRepo, IMapper mapper)
        {
            _studentRepo = studentRepo ?? throw new ArgumentNullException(nameof(studentRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ResponseEntity<IEnumerable<StudentVM>>> GetAllAsync()
        {
            var response = new ResponseEntity<IEnumerable<StudentVM>>();
            try
            {
                var students = await _studentRepo.GetAllAsync();
                response.Data = _mapper.Map<IEnumerable<StudentVM>>(students);
                response.StatusCode = 200; // OK
            }
            catch (Exception ex)
            {
                response.Errors = new List<string> { ex.Message };
                response.StatusCode = 500; // Internal Server Error
            }
            return response;
        }

        public async Task<ResponseEntity<StudentVM>> GetByIdAsync(int id)
        {
            var response = new ResponseEntity<StudentVM>();
            try
            {
                var student = await _studentRepo.GetByIdAsync(id);
                if (student == null)
                {
                    response.Errors = new List<string> { "Student not found" };
                    response.StatusCode = 404; // Not Found
                }
                else
                {
                    response.Data = _mapper.Map<StudentVM>(student);
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

        public async Task<ResponseEntity<StudentVM>> AddAsync(StudentVM student)
        {
            var response = new ResponseEntity<StudentVM>();
            try
            {
                student.IsDeleted = false;
                var studentModel = _mapper.Map<Student>(student);
                await _studentRepo.AddAsync(studentModel);
                response.Data = student;
                response.StatusCode = 201; // Created
            }
            catch (Exception ex)
            {
                response.Errors = new List<string> { ex.Message };
                response.StatusCode = 500; // Internal Server Error
            }
            return response;
        }

        public async Task<ResponseEntity<StudentVM>> UpdateAsync(StudentVM student, string jwtToken)
        {
            var user = JwtTokenHelper.ExtractClaimsFromToken(jwtToken);
            var response = new ResponseEntity<StudentVM>();
            try
            {
                student.ModifiedBy = user;
                student.ModifiedDate = DateTime.Now;
                student.IsDeleted = false;
                var studentModel = _mapper.Map<Student>(student);
                await _studentRepo.UpdateAsync(studentModel);
                response.Data = student;
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
                await _studentRepo.DeleteAsync(id, user);
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
