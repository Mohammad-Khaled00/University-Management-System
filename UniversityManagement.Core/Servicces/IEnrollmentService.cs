using UniversityManagement.Core.ViewModels;

namespace UniversityManagement.Core.Servicces
{
    public interface IEnrollmentService
    {
        Task<ResponseEntity<IEnumerable<EnrollmentVM>>> GetAllAsync();
        Task<ResponseEntity<EnrollmentVM>> GetByCourseAndStudentAsync(int courseId, int studentId);
        Task<ResponseEntity<EnrollmentVM>> AddAsync(EnrollmentVM enrollment, string jwtToken);
        Task<ResponseEntity<bool>> DeleteAsync(int courseId, int studentId, string jwtToken);
    }
}
