using AutoMapper;
using UniversityManagement.Core.Models;
using UniversityManagement.Core.ViewModels;

namespace UniversityManagement.app.MConfig
{
    public class InstructorMapper : Profile
    {
        public InstructorMapper()
        {
            CreateMap<Instructor, InstructorVM>()
                .ForMember(x => x.Name, opt => opt.MapFrom(i => i.FirstName + " " + i.LastName));

            CreateMap<InstructorVM, Instructor>()
                .ForMember(x => x.FirstName, opt => opt.MapFrom(i => GetFirstName(i.Name)))
                .ForMember(x => x.LastName, opt => opt.MapFrom(i => GetLastName(i.Name)));
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
