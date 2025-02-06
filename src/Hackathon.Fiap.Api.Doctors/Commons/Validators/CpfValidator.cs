using FluentValidation;
using Hackathon.Fiap.Infrastructure.Data.Config;

namespace Hackathon.Fiap.Api.Doctors.Commons.Validators;

public class CpfValidator : AbstractValidator<string>
{
    public CpfValidator()
    {
        RuleSet("Cpf", () =>
        {
            RuleFor(x => x)
                .NotEmpty()
                .MinimumLength(DataSchemaConstants.DEFAULT_CPF_LENGTH)
                .MaximumLength(DataSchemaConstants.DEFAULT_CPF_LENGTH)
                .Matches(@"^\d{11}$").WithMessage("CPF is in an incorrect format.")
                .Must(ValidateCpf).WithMessage("Invalid CPF.");
        });
    }

    private static bool ValidateCpf(string cpf)
    {
        int[] multiplicador1 = [10, 9, 8, 7, 6, 5, 4, 3, 2];
        int[] multiplicador2 = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];

        cpf = cpf.Trim().Replace(".", "").Replace("-", "");
        if (cpf.Length != 11)
            return false;

        for (var j = 0; j < 10; j++)
            if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                return false;

        var tempCpf = cpf.Substring(0, 9);
        var sum = 0;

        for (var i = 0; i < 9; i++)
            sum += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        var mod = sum % 11;
        if (mod < 2)
            mod = 0;
        else
            mod = 11 - mod;

        var digit = mod.ToString();
        tempCpf = tempCpf + digit;
        sum = 0;
        for (var i = 0; i < 10; i++)
            sum += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        mod = sum % 11;
        if (mod < 2)
            mod = 0;
        else
            mod = 11 - mod;

        digit = digit + mod.ToString();

        return cpf.EndsWith(digit);
    }
}
