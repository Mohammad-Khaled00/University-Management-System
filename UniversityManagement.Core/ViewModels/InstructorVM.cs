namespace UniversityManagement.Core.ViewModels
{
    public class InstructorVM
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Departement { get; set; }

        public string? PhoneNumber { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool? IsDeleted { get; set; }

        public string? DeletedBy { get; set; }

        public DateTime? DeleteDate { get; set; }

        public string? UsersId { get; set; }
    }
}
