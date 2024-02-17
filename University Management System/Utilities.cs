using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using UniversityManagement.app.MConfig;
using UniversityManagement.app.Services;
using UniversityManagement.Core.Repositories;
using UniversityManagement.Core.Servicces;
using UniversityManagement.Infrastructure.Data;
using UniversityManagement.Infrastructure.Repositories;

namespace University_Management_System
{
    public static class Utilities
    {
        public static void ConfigureServices(this IServiceCollection services, string connectionString)
        {
            //DB
            services.AddDbContext<UniverisityDbContext>(options => options.UseSqlServer(connectionString));
            services.AddTransient<IDbConnection>(provider => new SqlConnection(connectionString));

            //REPOS
            services.AddScoped<IStudentRepo, StudentRepo>();
            services.AddScoped<IInstructorRepo, InstructorRepo>();
            services.AddScoped<ICourseRepo, CourseRepo>();
            services.AddScoped<IEnrollmentRepo, EnrollmentRepo>();
            services.AddScoped<IUserRepository, UserRepository>();

            //Mappers
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new StudentMapper());
                mc.AddProfile(new InstructorMapper());
                mc.AddProfile(new CourseMapper());
                mc.AddProfile(new EnrollmentMapper());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            //Services
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            services.AddScoped<IInstructorService, InstructorService>();
            services.AddScoped<IStudentService, StudentService>();

            //JWT
            services.AddScoped<IJwtAuthenticationService, JwtAuthenticationService>();
        }
    }
}
