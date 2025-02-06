using Hackathon.Fiap.Core.Aggregates.Appointments;
using Hackathon.Fiap.Core.Aggregates.Appointments.Specifications;

namespace Hackathon.Fiap.UseCases.Appointments.PatientAppointments;

public class GetPatientAppointmentsHandler(IRepository<Appointment> repository) : IQueryHandler<GetPatientAppointmentsQuery, IEnumerable<AppointmentsDto>>
{
    private readonly IRepository<Appointment> _repository = repository;

    public async Task<IEnumerable<AppointmentsDto>> Handle(GetPatientAppointmentsQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var spec = new GetPatientAppointmentsByStatusSpec(request.PatientId, request.Status);
        var result = await _repository.ListAsync(spec, cancellationToken);

        return result.Select(x => new AppointmentsDto(x.Id, x.Start, x.End, x.Status, x.PatientId, x.Patient.Name, x.DoctorId, x.Doctor.Name));
    }
}
