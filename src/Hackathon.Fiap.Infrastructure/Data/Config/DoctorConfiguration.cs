using Hackathon.Fiap.Core.Aggregates.Doctors;

namespace Hackathon.Fiap.Infrastructure.Data.Config;
public sealed class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.Property(x => x.Name)
            .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
            .IsRequired();

        builder.Property(x => x.Crm)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.Cpf)
            .HasMaxLength(DataSchemaConstants.DEFAULT_CPF_LENGTH)
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithOne()
            .HasForeignKey(typeof(Doctor), nameof(Doctor.UserId));

        builder.HasMany(x => x.Schedules)
            .WithOne()
            .HasForeignKey(x => x.DoctorId);
    }
}
