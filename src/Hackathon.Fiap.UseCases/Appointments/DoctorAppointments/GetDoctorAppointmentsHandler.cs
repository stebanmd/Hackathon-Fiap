using Hackathon.Fiap.Core.Aggregates.Appointments;
using Hackathon.Fiap.Core.Aggregates.Appointments.Specifications;

namespace Hackathon.Fiap.UseCases.Appointments.DoctorAppointments;

public class GetDoctorAppointmentsHandler(IRepository<Appointment> repository) : IQueryHandler<GetDoctorAppointmentsQuery, IEnumerable<AppointmentsDto>>
{
    private readonly IRepository<Appointment> _repository = repository;

    public async Task<IEnumerable<AppointmentsDto>> Handle(GetDoctorAppointmentsQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var spec = new GetDoctorAppointmentsByStatusSpec(request.DoctorId, request.Status);
        var result = await _repository.ListAsync(spec, cancellationToken);

        return result.Select(x => new AppointmentsDto(x.Id, x.Start, x.End, x.Status, x.PatientId, x.Patient.Name));
    }
}
