namespace Hackathon.Fiap.Web.Endpoints.Contributors;

public class UpdateContributorResponse(ContributorRecord contributor)
{
    public ContributorRecord Contributor { get; set; } = contributor;
}
