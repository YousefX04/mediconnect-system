using Hospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Persistence.Configurations
{
    public class MedicalRecordConfig : IEntityTypeConfiguration<MedicalRecord>
    {
        public void Configure(EntityTypeBuilder<MedicalRecord> builder)
        {
            builder.HasKey(mr => mr.MedicalRecordId);

            builder.HasOne(mr => mr.Patient)
                   .WithMany(p => p.MedicalRecords)
                   .HasForeignKey(mr => mr.PatientId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(mr => mr.Doctor)
                   .WithMany(d => d.MedicalRecords)
                   .HasForeignKey(mr => mr.DoctorId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
