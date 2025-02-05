namespace Hackathon.Fiap.UseCases.Doctors.List;

/// <summary>
/// Represents a service that will actually fetch the necessary data
/// Typically implemented in Infrastructure
/// </summary>
public interface IListDoctorsQueryService
{
    Task<IEnumerable<DoctorDto>> ListAsync(int? specialtyId);
}
