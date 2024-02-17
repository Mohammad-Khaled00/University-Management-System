namespace UniversityManagement.Core.Repositories
{
    public interface IUserRepository
    {
        bool EmailExists(string email);
    }
}
