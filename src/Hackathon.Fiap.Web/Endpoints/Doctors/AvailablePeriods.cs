using Hackathon.Fiap.UseCases;
using Hackathon.Fiap.UseCases.Doctors.AvailablePeriod;

namespace Hackathon.Fiap.Web.Endpoints.Doctors;

/// <summary>
/// Get the available periods for a doctor on a specific date.
/// </summary>
public partial class AvailablePeriods(IMediator mediator) : Endpoint<GetAvailablePeriodsRequest, GetAvailablePeriodsResponse>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Get(GetAvailablePeriodsRequest.Route);
        Roles(ApplicationRoles.Patient);

        Summary(x =>
        {
            x.Response(StatusCodes.Status200OK, "The available periods for the doctor on the specified date.");
            x.Response(StatusCodes.Status400BadRequest, "Invalid doctor or selected date");
        });
    }

    public override async Task HandleAsync(GetAvailablePeriodsRequest req, CancellationToken ct)
    {
        var query = new GetAvailablePeriodsQuery(req.DoctorId, req.Date);
        var result = await _mediator.Send(query, ct);

        if (!result.IsSuccess)
        {
            result.Errors.ToList().ForEach(error => AddError(error));
            ThrowIfAnyErrors(StatusCodes.Status400BadRequest);
        }

        Response = new GetAvailablePeriodsResponse(result.Value.Price, result.Value.AvailableTimes);
    }
}
