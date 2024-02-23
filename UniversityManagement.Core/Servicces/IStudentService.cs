using Microsoft.AspNetCore.Identity;
using UniversityManagement.Core.ViewModels;

namespace UniversityManagement.Core.Servicces
{
    public interface IStudentService
    {
        Task<ResponseEntity<IEnumerable<StudentVM>>> GetAllAsync();
        Task<ResponseEntity<StudentVM>> GetByIdAsync(int id);
        Task<ResponseEntity<StudentVM>> AddAsync(StudentVM student);
        Task<ResponseEntity<StudentVM>> UpdateAsync(StudentVM student);
        Task<ResponseEntity<bool>> DeleteAsync(int id);
        Task<ResponseEntity<StudentVM>> Register(STUDRegestrationVM Data);
    }
}
