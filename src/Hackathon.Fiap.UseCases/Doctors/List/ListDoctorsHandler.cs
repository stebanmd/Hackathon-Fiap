namespace Hackathon.Fiap.UseCases.Doctors.List;

public class ListDoctorsHandler(IListDoctorsQueryService _query)
  : IQueryHandler<ListDoctorsQuery, Result<IEnumerable<DoctorDto>>>
{
    public async Task<Result<IEnumerable<DoctorDto>>> Handle(ListDoctorsQuery request, CancellationToken cancellationToken)
    {
        var result = await _query.ListAsync(request.SpecialtyId);

        return Result.Success(result);
    }
}
