using UniversityManagement.Core.Repositories;
using UniversityManagement.Infrastructure.Data;

namespace UniversityManagement.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UniverisityDbContext _dbContext;

        public UserRepository(UniverisityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool EmailExists(string email)
        {
            // Check if the email exists in table Instructors
            bool InstructorExists = _dbContext.Instructors.Any(x => x.Email == email);

            // Check if the email exists in table Students
            bool StudentExists = _dbContext.Students.Any(y => y.Email == email);

            // Return true if the email exists in either table Instructors or table Students
            return InstructorExists || StudentExists;
        }
    }

}
