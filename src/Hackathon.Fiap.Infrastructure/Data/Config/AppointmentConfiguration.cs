using Hackathon.Fiap.Core.Aggregates.Appointments;

namespace Hackathon.Fiap.Infrastructure.Data.Config;
public sealed class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.Property(x => x.Status)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder
            .HasOne(x => x.Patient)
            .WithMany()
            .HasForeignKey(x => x.PatientId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(x => x.Doctor)
            .WithMany()
            .HasForeignKey(x => x.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.DoctorId, x.Start, x.Status })
            .HasFilter($"[{nameof(Appointment.Status)}] = 'Confirmed'")
            .IsUnique();
    }
}
