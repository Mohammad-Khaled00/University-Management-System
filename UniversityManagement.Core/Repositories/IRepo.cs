namespace UniversityManagement.Core.Repositories
{
    public interface IRepo<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int Id);
        Task AddAsync(T t);
        Task UpdateAsync(T t);
        Task DeleteAsync(int id, string user);
    }
}
