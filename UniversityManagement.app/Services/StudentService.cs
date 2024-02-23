using AutoMapper;
using System.Transactions;
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
        private readonly IRegisterService registerService;
        private readonly IFetchTokenRepo _tokenRepo;

        public StudentService(IStudentRepo studentRepo, IMapper mapper, IRegisterService RegisterService, IFetchTokenRepo tokenRepo)
        {
            _studentRepo = studentRepo ?? throw new ArgumentNullException(nameof(studentRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            registerService = RegisterService;
            _tokenRepo = tokenRepo;
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

        public async Task<ResponseEntity<StudentVM>> UpdateAsync(StudentVM student)
        {
            var user = _tokenRepo.FetchClaims();
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

        public async Task<ResponseEntity<StudentVM>> Register(STUDRegestrationVM Data)
        {
            using TransactionScope transaction = new(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                TransactionManager.ImplicitDistributedTransactions = true;
                TransactionInterop.GetTransmitterPropagationToken(Transaction.Current);
                Data.student.Name = Data.Model.Username;
                var res = await registerService.RegisterAsync(Data.Model, "Student");
                if (res.Errors != null)
                    throw new Exception(res.Errors.FirstOrDefault());

                Data.student.UsersId = res.Data.Id;
                Data.student.Email = res.Data.Email;
                var student = await AddAsync(Data.student);
                if (student.Errors != null)
                    throw new Exception(student.Errors.FirstOrDefault());

                transaction.Complete();
                return student;
            }
            catch (Exception ex)
            {
                transaction.Dispose();
                return new ResponseEntity<StudentVM> { Errors = [$"{ex.Message}"] };
            }
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

        public async Task<ResponseEntity<bool>> DeleteAsync(int id)
        {
            var user = _tokenRepo.FetchClaims();
            var response = new ResponseEntity<bool>();
            try
            {
                await _studentRepo.DeleteAsync(id, user);
                response.Data = true;
                response.StatusCode = 200; // OK
                response.Errors = null;
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
