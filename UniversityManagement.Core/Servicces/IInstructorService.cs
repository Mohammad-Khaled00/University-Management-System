using UniversityManagement.Core.ViewModels;

namespace UniversityManagement.Core.Servicces
{
    public interface IInstructorService
    {
        Task<ResponseEntity<IEnumerable<InstructorVM>>> GetAllAsync();
        Task<ResponseEntity<InstructorVM>> GetByIdAsync(int id);
        Task<ResponseEntity<InstructorVM>> AddAsync(InstructorVM instructor);
        Task<ResponseEntity<InstructorVM>> UpdateAsync(InstructorVM instructor);
        Task<ResponseEntity<bool>> DeleteAsync(int id);
        Task<ResponseEntity<InstructorVM>> Register(INSRegestrationVM Data);
    }
}
