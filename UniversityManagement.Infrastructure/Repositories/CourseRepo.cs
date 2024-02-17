using Microsoft.EntityFrameworkCore;
using UniversityManagement.Core.Models;
using UniversityManagement.Core.Repositories;
using UniversityManagement.Infrastructure.Data;

namespace UniversityManagement.Infrastructure.Repositories
{
    public class CourseRepo : BaseRepo<Course>, ICourseRepo
    {

        public CourseRepo(UniverisityDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await DbSet.Include(x => x.Ins).Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            return await DbSet.Include(x => x.Ins).Where(x => x.IsDeleted == false).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Course course)
        {
            Course OldC = await GetByIdAsync(course.Id);
            OldC.ModifiedDate = DateTime.Now;
            OldC.IsDeleted = false;
            OldC.ModifiedBy = course.ModifiedBy;
            OldC.CourseName = course.CourseName;
            OldC.InsId = course.InsId;
            OldC.Departement = course.Departement;
            await base.UpdateAsync(OldC);
        }

        public async Task DeleteAsync(int id, string user)
        {
            Course course = await GetByIdAsync(id);
            if (course != null)
            {
                course.IsDeleted = true;
                course.DeleteDate = DateTime.Now;
                course.DeletedBy = user;
                await CorseEnrollments(course);
                DbSet.Update(course);
                await base.DeleteAsync(id, user);
            }
        }

        private async Task CorseEnrollments(Course course)
        {
            var enrollmentsToRemove = DbContext.Enrollments
                .Where(e => e.CrsId == course.Id).ToList();
            foreach (var enrollment in enrollmentsToRemove)
            {
                enrollment.IsDeleted = true;
                enrollment.DeleteDate = course.DeleteDate;
                enrollment.DeletedBy = course.DeletedBy;
            }
            DbContext.UpdateRange(enrollmentsToRemove);
        }
    }
}
