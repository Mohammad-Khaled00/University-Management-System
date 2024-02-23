using UniversityManagement.Core.ViewModels;

namespace UniversityManagement.Core.Servicces
{
    public interface ICourseService
    {
        Task<ResponseEntity<IEnumerable<CourseVM>>> GetAllAsync();
        Task<ResponseEntity<CourseVM>> GetByIdAsync(int id);
        Task<ResponseEntity<CourseVM>> AddAsync(CourseVM course);
        Task<ResponseEntity<CourseVM>> UpdateAsync(CourseVM course);
        Task<ResponseEntity<bool>> DeleteAsync(int id);
    }
}
