using Ardalis.Result;
using Hackathon.Fiap.Core.Aggregates.Appointments;
using Hackathon.Fiap.Core.Aggregates.Appointments.Specifications;
using Hackathon.Fiap.Core.Aggregates.Doctors;
using Hackathon.Fiap.Core.Aggregates.Doctors.Specifications;
using Hackathon.Fiap.UseCases.Doctors.AvailablePeriod;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Hackathon.Fiap.UnitTests.UseCases.Doctors;
public class GetAvailablePeriodsTests
{
    private readonly IRepository<Doctor> _doctorRepository = Substitute.For<IRepository<Doctor>>();
    private readonly IRepository<Appointment> _appointmentRepository = Substitute.For<IRepository<Appointment>>();
    private readonly ILogger<GetAvailablePeriodsHandler> _logger = NullLogger<GetAvailablePeriodsHandler>.Instance;

    private readonly GetAvailablePeriodsHandler _handler;

    public GetAvailablePeriodsTests()
    {
        _handler = new GetAvailablePeriodsHandler(_doctorRepository, _appointmentRepository, _logger);
    }

    [Fact]
    public async Task Return_NotFound()
    {
        _doctorRepository.FirstOrDefaultAsync(Arg.Any<GetDoctorByIdSpec>(), Arg.Any<CancellationToken>())
            .Returns(default(Doctor));

        var result = await _handler.Handle(new GetAvailablePeriodsQuery(1, new DateOnly(2022, 1, 1)), CancellationToken.None);
        Assert.NotNull(result);
        Assert.True(result.IsNotFound());
    }

    [Fact]
    public async Task Return_ListOfAvailableTimes()
    {
        // Arrange
        var doctor = new Doctor("Name", "cpf", "crm");
        var dateFilter = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1);

        doctor.AddSchedule(new()
        {
            Day = dateFilter,
            Start = new TimeOnly(8, 0),
            End = new TimeOnly(11, 0)
        });

        doctor.SetAppointmentConfiguration(new()
        {
            Duration = 30,
            Price = 100
        });

        var appointments = new List<Appointment>
        {
            new
            (
                dateFilter.ToDateTime(new(9, 0)),
                dateFilter.ToDateTime(new(9, 30))
            )
        };

        _doctorRepository.FirstOrDefaultAsync(Arg.Any<GetDoctorByIdSpec>(), Arg.Any<CancellationToken>())
            .Returns(doctor);

        _appointmentRepository.ListAsync(Arg.Any<GetDoctorAppointmentsByDate>(), Arg.Any<CancellationToken>())
            .Returns(appointments);

        // Act
        var result = await _handler.Handle(new GetAvailablePeriodsQuery(1, dateFilter), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotEmpty(result.Value.AvailableTimes);
        Assert.Equal(100, result.Value.Price);

        List<TimeOnly> expectedTimes =
            [
                new TimeOnly(8, 0),
                new TimeOnly(8, 30),
                new TimeOnly(9, 30),
                new TimeOnly(10, 0),
                new TimeOnly(10, 30),
            ];

        Assert.Equivalent(expectedTimes, result.Value.AvailableTimes);
    }
}
