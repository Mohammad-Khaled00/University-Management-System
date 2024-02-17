using System.Text.Json.Serialization;

namespace UniversityManagement.Core.ViewModels
{
    public class EnrollmentVM
    {
        public int StudId { get; set; }

        public string? StdName { get; set; }

        public int CrsId { get; set; }

        public string? CrsName { get; set; }

        public string? CreatedBy { get; set; }

        public DateOnly? EnrollmentDate { get; set; }

        public bool? IsDeleted { get; set; }

        public string? DeletedBy { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}
