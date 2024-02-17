using AutoMapper;
using UniversityManagement.Core.Models;
using UniversityManagement.Core.ViewModels;

namespace UniversityManagement.app.MConfig
{
    public class StudentMapper : Profile
    {
        public StudentMapper() {
            CreateMap<Student, StudentVM>()
                .ForMember(x => x.Name, opt => opt.MapFrom(s => s.FirstName + " " + s.LastName));

            CreateMap<StudentVM, Student>()
                .ForMember(x => x.FirstName, opt => opt.MapFrom(s => GetFirstName(s.Name)))
                .ForMember(x => x.LastName, opt => opt.MapFrom(s => GetLastName(s.Name)));
        }
        private static string GetFirstName(string fullName)
        {
            return fullName?.Split(' ')?.FirstOrDefault();
        }

        private static string GetLastName(string fullName)
        {
            return fullName?.Split(' ')?.LastOrDefault();
        }
    }
}
