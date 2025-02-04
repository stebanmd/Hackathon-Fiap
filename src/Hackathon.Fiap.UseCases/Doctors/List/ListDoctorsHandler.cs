namespace Hackathon.Fiap.UseCases.Doctors.List;

public class ListDoctorsHandler(IListDoctorsQueryService _query)
  : IQueryHandler<ListDoctorsQuery, Result<IEnumerable<DoctorDto>>>
{
    public async Task<Result<IEnumerable<DoctorDto>>> Handle(ListDoctorsQuery request, CancellationToken cancellationToken)
    {
        var result = await _query.ListAsync();

        return Result.Success(result);
    }
}
