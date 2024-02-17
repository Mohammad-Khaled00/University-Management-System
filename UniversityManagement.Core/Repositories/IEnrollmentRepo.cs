using UniversityManagement.Core.Models;

namespace UniversityManagement.Core.Repositories
{
    public interface IEnrollmentRepo
    {
        Task<IEnumerable<Enrollment>> GetAllAsync();
        Task<Enrollment> GetByCourseAndStudentAsync(int courseId, int studentId);
        Task AddAsync(Enrollment enrollment);
        Task DeleteAsync(int courseId, int studentId, string user);
    }
}
