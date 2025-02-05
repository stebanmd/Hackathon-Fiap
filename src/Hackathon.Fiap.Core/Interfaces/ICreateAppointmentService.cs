using Hackathon.Fiap.Core.Aggregates.Doctors;
using Hackathon.Fiap.Core.Aggregates.Patients;

namespace Hackathon.Fiap.Core.Interfaces;

public interface ICreateAppointmentService
{
    Task<int> Create(Patient patient, Doctor doctor, DateTime start, DateTime end);
}
