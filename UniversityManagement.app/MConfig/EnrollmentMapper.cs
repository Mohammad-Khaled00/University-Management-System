using AutoMapper;
using UniversityManagement.Core.Models;
using UniversityManagement.Core.ViewModels;

namespace UniversityManagement.app.MConfig
{
    public class EnrollmentMapper : Profile
    {
        public EnrollmentMapper()
        {
            CreateMap<Enrollment, EnrollmentVM>()
            .ForMember(x => x.StdName, opt => opt.MapFrom(e => e.Stud.FirstName + " " + e.Stud.LastName))
            .ForMember(x => x.CrsName, opt => opt.MapFrom(e => e.Crs.CourseName))
            .ForMember(x => x.EnrollmentDate, opt => opt.MapFrom<CurrentDateResolver>());

            CreateMap<EnrollmentVM, Enrollment>();
        }
    }

/*        .ForMember(x => x.EnrollmentDate, opt => opt.MapFrom(e => e.EnrollmentDate));

        *//*CreateMap<EnrollmentVM, Enrollment>()
            .ForMember(x => x.EnrollmentDate, opt => opt.MapFrom(e =>
                new DateOnly(e.EnrollmentDate.Year, e.EnrollmentDate.Month, e.EnrollmentDate.Day)));*//*
        CreateMap<EnrollmentVM, Enrollment>()
            .ForMember(dest => dest.EnrollmentDate, opt => opt.MapFrom(src => MapEnrollmentDate(src.EnrollmentDate)));

    }
    private DateOnly MapEnrollmentDate(EnrollmentDateDTO enrollmentDateDTO)
    {
        return new DateOnly(enrollmentDateDTO.Year, enrollmentDateDTO.Month, enrollmentDateDTO.Day);
    }
}*/

public class CurrentDateResolver : IValueResolver<Enrollment, EnrollmentVM, DateOnly?>
    {
        public DateOnly? Resolve(Enrollment source, EnrollmentVM destination, DateOnly? destMember, ResolutionContext context)
        {
            return DateOnly.FromDateTime(DateTime.UtcNow.Date);
        }
    }
}
