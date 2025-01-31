using Hackathon.Fiap.Core.Aggregates.Patients;

namespace Hackathon.Fiap.Infrastructure.Data.Config;

public sealed class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.Property(x => x.Name)
            .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
            .IsRequired();

        builder.Property(x => x.Cpf)
            .HasMaxLength(DataSchemaConstants.DEFAULT_CPF_LENGTH)
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithOne()
            .HasForeignKey(typeof(Patient), nameof(Patient.UserId));
    }
}
