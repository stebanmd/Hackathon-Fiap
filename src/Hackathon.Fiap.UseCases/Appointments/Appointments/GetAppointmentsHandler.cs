using Hackathon.Fiap.Core.Aggregates.Appointments;
using Hackathon.Fiap.Core.Aggregates.Appointments.Specifications;

namespace Hackathon.Fiap.UseCases.Appointments.Appointments;

public class GetAppointmentsHandler(IRepository<Appointment> repository) : IQueryHandler<GetAppointmentsQuery, IEnumerable<AppointmentsDto>>
{
    private readonly IRepository<Appointment> _repository = repository;

    public async Task<IEnumerable<AppointmentsDto>> Handle(GetAppointmentsQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var spec = new GetAppointmentsByStatusSpec(request.DoctorId, request.Status);
        var result = await _repository.ListAsync(spec, cancellationToken);

        return result.Select(x => new AppointmentsDto(x.Id, x.Start, x.End, x.Status, x.PatientId, x.Patient.Name));
    }
}
