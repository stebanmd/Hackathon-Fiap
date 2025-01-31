using FluentValidation;
using Hackathon.Fiap.Infrastructure.Data.Config;

namespace Hackathon.Fiap.Web.Endpoints.Patients;

public class UpdatePatientValidator : Validator<UpdatePatientRequest>
{
    public UpdatePatientValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
    }
}
