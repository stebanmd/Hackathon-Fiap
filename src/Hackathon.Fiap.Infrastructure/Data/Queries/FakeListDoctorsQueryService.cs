using Hackathon.Fiap.UseCases.Doctors;
using Hackathon.Fiap.UseCases.Doctors.List;

namespace Hackathon.Fiap.Infrastructure.Data.Queries;

public class FakeListDoctorsQueryService : IListDoctorsQueryService
{
    public Task<IEnumerable<DoctorDto>> ListAsync(int? specialtyId)
    {
        IEnumerable<DoctorDto> result =
        [
            new DoctorDto(1, "Fake Doctor 1", "12332452351", "123456789", specialtyId),
            new DoctorDto(2, "Fake Doctor 2", "98632452351", "123456789", specialtyId),
        ];

        return Task.FromResult(result);
    }
}
