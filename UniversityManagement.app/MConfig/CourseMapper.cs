using AutoMapper;
using UniversityManagement.Core.Models;
using UniversityManagement.Core.ViewModels;

namespace UniversityManagement.app.MConfig
{
    public class CourseMapper : Profile
    {
        public CourseMapper()
        {
            CreateMap<Course, CourseVM>()
                .ForMember(x => x.InsName, opt => opt.MapFrom(c => c.Ins.FirstName + " " + c.Ins.LastName));

            CreateMap<CourseVM, Course>();
        }
    }
}
