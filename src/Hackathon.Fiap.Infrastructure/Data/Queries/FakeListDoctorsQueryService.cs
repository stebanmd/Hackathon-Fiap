using Hackathon.Fiap.UseCases.Doctors;
using Hackathon.Fiap.UseCases.Doctors.List;

namespace Hackathon.Fiap.Infrastructure.Data.Queries;

public class FakeListDoctorsQueryService : IListDoctorsQueryService
{
    public Task<IEnumerable<DoctorDto>> ListAsync()
    {
        IEnumerable<DoctorDto> result =
        [
            new DoctorDto(1, "Fake Doctor 1", "12332452351", "123456789"),
            new DoctorDto(2, "Fake Doctor 2", "98632452351", "123456789")
        ];

        return Task.FromResult(result);
    }
}
