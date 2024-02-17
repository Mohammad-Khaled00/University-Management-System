using Dapper;
using System.Data;
using UniversityManagement.Core.Models;
using UniversityManagement.Core.Repositories;

namespace UniversityManagement.Infrastructure.Repositories
{
    public class StudentRepo : IStudentRepo
    {
        private readonly IDbConnection DbConnection;

        public StudentRepo(IDbConnection dbConnection)
        {
            DbConnection = dbConnection;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await DbConnection.QueryAsync<Student>("SELECT * FROM Students WHERE IsDeleted = 0");
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            return await DbConnection.QueryFirstOrDefaultAsync<Student>("SELECT * FROM Students WHERE Id = @Id AND IsDeleted = 0", new { Id = id });

        }

        public async Task AddAsync(Student student)
        {
            await DbConnection.ExecuteAsync("INSERT INTO Students (FirstName, LastName, Age, Email, PhoneNumber, IsDeleted) VALUES (@FirstName, @LastName, @Age, @Email, @PhoneNumber, @IsDeleted)", student);
        }

        public async Task UpdateAsync(Student student)
        {
            var properties = typeof(Student).GetProperties()
                .Where(p => p.Name != "Id" && p.Name != "Enrollments" && p.GetValue(student) != null);
            var updateColumns = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));

            var sql = $"UPDATE Students SET {updateColumns} WHERE Id = @Id";

            await DbConnection.ExecuteAsync(sql, student);
        }

        public async Task DeleteAsync(int id, string user)
        {
            var student = await GetByIdAsync(id);
            if (student != null)
            {
                student.IsDeleted = true;
                student.DeleteDate = DateTime.Now;
                student.DeletedBy = user;

                var sql = @"UPDATE Students 
                    SET IsDeleted = @IsDeleted, DeleteDate = @DeleteDate, DeletedBy = @DeletedBy
                    WHERE Id = @Id";

                await DbConnection.ExecuteAsync(sql, new
                {
                    student.Id,
                    student.IsDeleted,
                    student.DeleteDate,
                    student.DeletedBy
                });
            }
            //await DbConnection.ExecuteAsync("UPDATE Students SET FirstName = @FirstName, LastName = @LastName, Age = @Age, Email = @Email, PhoneNumber = @PhoneNumber WHERE Id = @Id", student);

            /*        public async Task DeleteAsync(Student student)
                    {
                        await UpdateAsync(student);
                        //await DbConnection.ExecuteAsync("DELETE FROM Enrollments WHERE StudID = @Id", new { Id = id });
                        //await DbConnection.ExecuteAsync("DELETE FROM Students WHERE Id = @Id", new { Id = id });
                    }*/
        }
    }
}
