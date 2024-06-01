using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Evaluation_venussoftop.Models;

public partial class VenussoftopEvaluationContext : DbContext
{
    public VenussoftopEvaluationContext()
    {
    }

    public VenussoftopEvaluationContext(DbContextOptions<VenussoftopEvaluationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<PatientEmail> PatientEmails { get; set; }

    public virtual DbSet<PatientPhoneNumber> PatientPhoneNumbers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)=> optionsBuilder.UseSqlServer("Server=LAPTOP-6FBJ3NLA;Database=Venussoftop-Evaluation;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Patient__3214EC07C1C45526");

            entity.ToTable("Patient");

            entity.Property(e => e.Dob).HasColumnType("datetime");
            entity.Property(e => e.Firstname).HasMaxLength(250);
            entity.Property(e => e.Prefix)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Surname).HasMaxLength(250);
        });

        modelBuilder.Entity<PatientEmail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Patient___3214EC07617CCEEF");

            entity.ToTable("Patient_Email");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PatientId).HasColumnName("PatientID");

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientEmails)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK__Patient_E__Patie__3D5E1FD2");
        });

        modelBuilder.Entity<PatientPhoneNumber>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Patient___3214EC0759A1855B");

            entity.ToTable("Patient_PhoneNumber");

            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientPhoneNumbers)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK__Patient_P__Patie__403A8C7D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
