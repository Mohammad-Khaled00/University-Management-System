using AutoMapper;
using UniversityManagement.Core.Models;
using UniversityManagement.Core.Repositories;
using UniversityManagement.Core.Servicces;
using UniversityManagement.Core.ViewModels;

namespace UniversityManagement.app.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly IInstructorRepo _instructorRepo;
        private readonly IMapper _mapper;

        public InstructorService(IInstructorRepo instructorRepo, IMapper mapper)
        {
            _instructorRepo = instructorRepo ?? throw new ArgumentNullException(nameof(instructorRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ResponseEntity<IEnumerable<InstructorVM>>> GetAllAsync()
        {
            var response = new ResponseEntity<IEnumerable<InstructorVM>>();
            try
            {
                var instructors = await _instructorRepo.GetAllAsync();
                response.Data = _mapper.Map<IEnumerable<InstructorVM>>(instructors);
                response.StatusCode = 200; // OK
            }
            catch (Exception ex)
            {
                response.Errors = new List<string> { ex.Message };
                response.StatusCode = 500; // Internal Server Error
            }
            return response;
        }

        public async Task<ResponseEntity<InstructorVM>> GetByIdAsync(int id)
        {
            var response = new ResponseEntity<InstructorVM>();
            try
            {
                var instructor = await _instructorRepo.GetByIdAsync(id);
                if (instructor == null)
                {
                    response.Errors = new List<string> { "Instructor not found" };
                    response.StatusCode = 404; // Not Found
                }
                else
                {
                    response.Data = _mapper.Map<InstructorVM>(instructor);
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

        public async Task<ResponseEntity<InstructorVM>> AddAsync(InstructorVM instructor)
        {
            var response = new ResponseEntity<InstructorVM>();
            try
            {
                instructor.IsDeleted = false;
                var instructorModel = _mapper.Map<Instructor>(instructor);
                await _instructorRepo.AddAsync(instructorModel);
                response.Data = instructor;
                response.StatusCode = 200; // OK
            }
            catch (Exception ex)
            {
                response.Errors = new List<string> { ex.Message };
                response.StatusCode = 500; // Internal Server Error
            }
            return response;
        }

        public async Task<ResponseEntity<InstructorVM>> UpdateAsync(InstructorVM instructor, string jwtToken)
        {
            var user = JwtTokenHelper.ExtractClaimsFromToken(jwtToken);
            var response = new ResponseEntity<InstructorVM>();
            try
            {
/*                ResponseEntity<InstructorVM> entity = await GetByIdAsync(instructor.Id);
                if (entity.Data == null)
                {
                    throw new Exception("This Instructor Dosen't Exist.");
                    //response.StatusCode = 404; // Not Found
                    //response.Errors = new List<string> { "This Instructor Dosen't Exist" };
                }*/

                instructor.ModifiedBy = user;
                instructor.ModifiedDate = DateTime.Now;
                instructor.IsDeleted = false;
/*                entity.Data.Name = instructor.Name;
                entity.Data.Email = instructor.Email;
                entity.Data.PhoneNumber = instructor.PhoneNumber;
                entity.Data.Departement = instructor.Departement;*/
                var instructorModel = _mapper.Map<Instructor>(instructor);
                await _instructorRepo.UpdateAsync(instructorModel);
                response.Data = instructor;
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
                    await _instructorRepo.DeleteAsync(id, user);
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
