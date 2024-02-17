namespace UniversityManagement.Core.ViewModels
{
    public class CourseVM
    {
        public int Id { get; set; }

        public string? CourseName { get; set; }

        public string? Departement { get; set; }

        public int? InsId { get; set; }

        public string? InsName { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool? IsDeleted { get; set; }

        public string? DeletedBy { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}
