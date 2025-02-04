using Hackathon.Fiap.Core.Aggregates.Doctors;

namespace Hackathon.Fiap.Infrastructure.Data.Config;

public class SpecialtyConfiguration : IEntityTypeConfiguration<Specialty>
{
    public void Configure(EntityTypeBuilder<Specialty> builder)
    {
        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasMany(x => x.Doctors)
            .WithOne(x => x.Specialty)
            .HasForeignKey(x => x.SpecialtyId);
    }
}
