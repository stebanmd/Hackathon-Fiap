using Hackathon.Fiap.Core.Aggregates.Appointments;
using Hackathon.Fiap.Core.Aggregates.Appointments.Events;
using Hackathon.Fiap.Core.Aggregates.Doctors;
using Hackathon.Fiap.Core.Aggregates.Patients;
using Hackathon.Fiap.Core.Services;

namespace Hackathon.Fiap.UnitTests.Core.Services;
public class CreateAppointmentServiceTests
{
    private readonly IRepository<Appointment> _repository = Substitute.For<IRepository<Appointment>>();
    private readonly IMediator _mediator = Substitute.For<IMediator>();

    private readonly CreateAppointmentService _service;

    public CreateAppointmentServiceTests()
    {
        _service = new CreateAppointmentService(_repository, _mediator);
    }

    [Fact]
    public async Task PublishEventAndAddsAppointment()
    {
        var doctor = new Doctor("Doctor", "cpf", "crm");
        var patient = new Patient("Patient", "cpf");

        var start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day+1, 10, 0, 0, DateTimeKind.Unspecified);
        var end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day+1, 11, 0, 0, DateTimeKind.Unspecified);


        var appointment = new Appointment(start, end);

        _repository.AddAsync(Arg.Any<Appointment>(), CancellationToken.None)
            .Returns(Task.FromResult(appointment));

        await _service.Create(patient, doctor, start, end);

        _ = _repository.Received().AddAsync(Arg.Any<Appointment>(), CancellationToken.None);
        _ = _mediator.ReceivedWithAnyArgs(1).Publish(Arg.Any<CreateAppointmentEvent>(), CancellationToken.None);
    }
}
