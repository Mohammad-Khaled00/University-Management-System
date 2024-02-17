using Microsoft.EntityFrameworkCore;
using UniversityManagement.Core.Models;
using UniversityManagement.Core.Repositories;
using UniversityManagement.Infrastructure.Data;

namespace UniversityManagement.Infrastructure.Repositories
{
    public class InstructorRepo : BaseRepo<Instructor>, IInstructorRepo
    {
        public InstructorRepo(UniverisityDbContext dbContext) : base(dbContext) { }
        
        public async Task<IEnumerable<Instructor>> GetAllAsync()
        {
            return await DbSet.Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<Instructor> GetByIdAsync(int id)
        {
            return await DbSet.Where(x => x.IsDeleted == false).FirstOrDefaultAsync(x => x.Id == id);
        }
/*
        public async Task UpdateAsync(Instructor instructor)
        {
            instructor.
            await base.UpdateAsync(entity);

        }*/

        public async Task DeleteAsync(int id, string user)
        {
            Instructor instructor = await GetByIdAsync(id);
            if (instructor != null)
            {
                instructor.IsDeleted = true;
                instructor.DeleteDate = DateTime.Now;
                instructor.DeletedBy = user;
                await InstructorCorses(instructor);
                DbSet.Update(instructor);
                await base.DeleteAsync(id, user);
            }
        }

        //separation of conserns is still an option
        private async Task InstructorCorses(Instructor instructor)
        {
            var coursesToRemove = DbContext.Courses
                .Where(i => i.InsId == instructor.Id).ToList();
            foreach (var course in coursesToRemove)
            {
                course.IsDeleted = true;
                course.DeleteDate = instructor.DeleteDate;
                course.DeletedBy = instructor.DeletedBy;
            }
            DbContext.UpdateRange(coursesToRemove);
        }
    }
}
