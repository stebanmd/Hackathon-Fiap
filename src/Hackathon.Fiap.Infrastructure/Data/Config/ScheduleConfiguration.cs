using Hackathon.Fiap.Core.Aggregates.Doctors;

namespace Hackathon.Fiap.Infrastructure.Data.Config;

public sealed class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder
            .Property(x => x.DayOfWeek)
            .HasConversion<string>()
            .HasMaxLength(10);
    }
}
