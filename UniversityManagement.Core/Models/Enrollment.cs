namespace UniversityManagement.Core.Models;

public partial class Enrollment
{
    public int StudId { get; set; }

    public int CrsId { get; set; }

    public DateOnly? EnrollmentDate { get; set; }

    public string? CreatedBy { get; set; }

    public bool? IsDeleted { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime? DeleteDate { get; set; }

    public virtual Course Crs { get; set; } = null!;

    public virtual Student Stud { get; set; } = null!;
}
