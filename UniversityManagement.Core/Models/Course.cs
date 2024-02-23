using System;
using System.Collections.Generic;

namespace UniversityManagement.Core.Models;

public partial class Course
{
    public int Id { get; set; }

    public string? CourseName { get; set; }

    public string? Departement { get; set; }

    public int? InsId { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime? DeleteDate { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual Instructor? Ins { get; set; }
}
