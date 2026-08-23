using MedicalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<PatientRecord> PatientRecords { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Examination> Examinations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>()
                .Property(p => p.DateOfBirth)
                .HasColumnType("date");

            modelBuilder.Entity<Patient>()
                .HasIndex(p => p.OIB)
                .IsUnique();

            modelBuilder.Entity<PatientRecord>()
                .Property(pr => pr.StartDate)
                .HasColumnType("date");

            modelBuilder.Entity<PatientRecord>()
                .Property(pr => pr.EndDate)
                .HasColumnType("date");

            modelBuilder.Entity<PatientRecord>()
                .HasOne(pr => pr.Patient)
                .WithMany()
                .HasForeignKey(pr => pr.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PatientRecord>()
                .HasOne(pr => pr.Disease)
                .WithMany()
                .HasForeignKey(pr => pr.DiseaseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Patient)
                .WithMany()
                .HasForeignKey(p => p.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Doctor)
                .WithMany()
                .HasForeignKey(p => p.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Medication)
                .WithMany()
                .HasForeignKey(p => p.MedicationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Examination>()
                .HasOne(e => e.Patient)
                .WithMany()
                .HasForeignKey(e => e.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Examination>()
                .HasOne(e => e.Doctor)
                .WithMany()
                .HasForeignKey(e => e.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}