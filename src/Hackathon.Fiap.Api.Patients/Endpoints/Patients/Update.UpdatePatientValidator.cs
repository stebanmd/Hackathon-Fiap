using Hackathon.Fiap.Infrastructure.Data.Config;

namespace Hackathon.Fiap.Api.Patients.Endpoints.Patients;

public class UpdatePatientValidator : Validator<UpdatePatientRequest>
{
    public UpdatePatientValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
    }
}
