namespace Hackathon.Fiap.Api.Doctors.Endpoints.Contributors;

public class UpdateContributorResponse(ContributorRecord contributor)
{
    public ContributorRecord Contributor { get; set; } = contributor;
}
