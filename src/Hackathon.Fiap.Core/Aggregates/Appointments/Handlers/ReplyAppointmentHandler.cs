using System.Text;
using Hackathon.Fiap.Core.Aggregates.Appointments.Events;
using Hackathon.Fiap.Core.Interfaces;

namespace Hackathon.Fiap.Core.Aggregates.Appointments.Handlers;
internal class ReplyAppointmentHandler(IEmailSender emailSender, ILogger<ReplyAppointmentHandler> logger) : INotificationHandler<ReplyAppointmentEvent>
{
    private readonly IEmailSender _emailSender = emailSender;
    private readonly ILogger<ReplyAppointmentHandler> _logger = logger;

    public async Task Handle(ReplyAppointmentEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Appointment was {Status}. Sending email notifying the patient", notification.Appointment.Status);

        var appointment = notification.Appointment;
        var doctor = appointment.Doctor;
        var patient = appointment.Patient;

        var statusMessage = appointment.Status switch
        {
            AppointmentStatus.Pending => "Pendente de confirmação",
            AppointmentStatus.Confirmed => "Confirmada",
            AppointmentStatus.Canceled => "Cancelada",
            _ => throw new NotImplementedException(),
        };

        var emailBody = new StringBuilder();
        emailBody.AppendLine($"Olá, {patient.Name}!");
        emailBody.AppendLine($"Sua consulta agendadata para {appointment.Start:dd/MM/yyyy} às {appointment.Start:HH:mm}, com o doutor {doctor.Name} teve o status alterado para {statusMessage}.");
        if (!string.IsNullOrEmpty(appointment.Reason))
        {
            emailBody.AppendLine($"Motivo:");
            emailBody.AppendLine(appointment.Reason);
        }

        await _emailSender
            .SendEmailAsync(patient.User.Email!,
                            "noreply@healthmed.com",
                            "Health&Med - Resposta de agendamento de consulta",
                            emailBody.ToString());
    }
}
