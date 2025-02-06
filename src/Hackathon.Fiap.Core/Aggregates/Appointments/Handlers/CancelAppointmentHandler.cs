using System.Text;
using Hackathon.Fiap.Core.Aggregates.Appointments.Events;
using Hackathon.Fiap.Core.Interfaces;

namespace Hackathon.Fiap.Core.Aggregates.Appointments.Handlers;
internal class CancelAppointmentHandler(IEmailSender emailSender, ILogger<CancelAppointmentHandler> logger) : INotificationHandler<CancelAppointmentEvent>
{
    private readonly IEmailSender _emailSender = emailSender;
    private readonly ILogger<CancelAppointmentHandler> _logger = logger;

    public async Task Handle(CancelAppointmentEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Appointment was canceled. Sending email notifying the responsable doctor");

        var appointment = notification.Appointment;
        var doctor = appointment.Doctor;
        var patient = appointment.Patient;

        var emailBody = new StringBuilder();
        emailBody.AppendLine($"Olá, Dr. {doctor.Name}!");
        emailBody.AppendLine($"Sua consulta agendadata para {appointment.Start:dd/MM/yyyy} às {appointment.Start:HH:mm}, com o paciente {patient.Name} foi cancelada.");
        emailBody.AppendLine($"O cancelamento foi solicitado pelo paciente que alegou:");
        emailBody.AppendLine(appointment.Reason);

        await _emailSender
            .SendEmailAsync(doctor.User.Email!,
                            "noreply@healthmed.com",
                            "Health&Med - Consulta cancelada por solitação do paciente",
                            emailBody.ToString());
    }
}
