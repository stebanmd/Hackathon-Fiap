using Hackathon.Fiap.Core.Interfaces;

namespace Hackathon.Fiap.Infrastructure.Email;

public class FakeEmailSender(ILogger<FakeEmailSender> logger) : IEmailSender
{
    private readonly ILogger<FakeEmailSender> _logger = logger;

    public Task SendEmailAsync(string to, string from, string subject, string body)
    {
        _logger.LogInformation("Not actually sending an email to {To} from {From} with subject {Subject}", to, from, subject);
        return Task.CompletedTask;
    }
}
