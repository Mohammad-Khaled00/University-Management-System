using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniversityManagement.Core.Models;

namespace UniversityManagement.Infrastructure.Data;

public partial class UniverisityDbContext : IdentityDbContext<IdentityUser>
{
    public UniverisityDbContext()
    {
    }

    public UniverisityDbContext(DbContextOptions<UniverisityDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Instructor> Instructors { get; set; }

    public virtual DbSet<RequestResponseLog> RequestResponseLogs { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=Univerisity_DB;Trusted_Connection=true;Encrypt=true;Trust Server Certificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Courses__3214EC27FACC0726");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CourseName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeleteDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Departement)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.InsId).HasColumnName("InsID");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Ins).WithMany(p => p.Courses)
                .HasForeignKey(d => d.InsId)
                .HasConstraintName("FK__Courses__InsID__656C112C");
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => new { e.StudId, e.CrsId }).HasName("PK__Enrollme__3A6C8B74239E2F5D");

            entity.Property(e => e.StudId).HasColumnName("StudID");
            entity.Property(e => e.CrsId).HasColumnName("CrsID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DeleteDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Crs).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.CrsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Enrollmen__CrsID__693CA210");

            entity.HasOne(d => d.Stud).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.StudId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Enrollmen__StudI__68487DD7");
        });

        modelBuilder.Entity<Instructor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Instruct__3214EC275B7FB45D");

            entity.HasIndex(e => e.Email, "UC_Email").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DeleteDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Departement)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.UsersId)
                .HasMaxLength(450)
                .HasColumnName("UsersID");
        });

        modelBuilder.Entity<RequestResponseLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RequestR__3214EC078EB523A3");

            entity.ToTable("RequestResponseLog");

            entity.Property(e => e.LogDateTime).HasColumnType("datetime");
            entity.Property(e => e.RequestMethod).HasMaxLength(10);
            entity.Property(e => e.RequestUrl).HasMaxLength(1000);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Students__3214EC27428FD023");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DeleteDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.UsersId)
                .HasMaxLength(450)
                .HasColumnName("UsersID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
