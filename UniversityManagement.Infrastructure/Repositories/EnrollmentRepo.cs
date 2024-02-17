using Microsoft.EntityFrameworkCore;
using UniversityManagement.Core.Models;
using UniversityManagement.Core.Repositories;
using UniversityManagement.Infrastructure.Data;

namespace UniversityManagement.Infrastructure.Repositories
{
    public class EnrollmentRepo : IEnrollmentRepo
    {
        private readonly UniverisityDbContext DbContext;
        private readonly DbSet<Enrollment> DbSet;
        public EnrollmentRepo(UniverisityDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Enrollments;
        }

        public async Task<IEnumerable<Enrollment>> GetAllAsync()
        {
            return await DbSet
                .Include(x => x.Stud)
                .Include(x => x.Crs)
                .Where(x => x.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<Enrollment> GetByCourseAndStudentAsync(int courseId, int studentId)
        {
            return await DbSet
                .Include(x => x.Stud)
                .Include(x => x.Crs)
                .Where(x => x.IsDeleted == false)
                .FirstOrDefaultAsync(x => x.CrsId == courseId && x.StudId == studentId);
        }

        public async Task AddAsync(Enrollment enrollment)
        {
            await DbSet.AddAsync(enrollment);
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int courseId, int studentId, string user)
        {
            var enrollment = await DbSet.FindAsync(courseId, studentId);
            if (enrollment != null)
            {
                enrollment.IsDeleted = true;
                enrollment.DeleteDate = DateTime.Now;
                enrollment.DeletedBy = user;
                DbSet.Update(enrollment);
                await DbContext.SaveChangesAsync();
            }
        }
    }
}
