using System.Text;
using Hackathon.Fiap.Core.Aggregates.Appointments.Events;
using Hackathon.Fiap.Core.Aggregates.Doctors;
using Hackathon.Fiap.Core.Aggregates.Patients;
using Hackathon.Fiap.Core.Interfaces;

namespace Hackathon.Fiap.Core.Aggregates.Appointments.Handlers;

internal class CreateAppointmentHandler(ILogger<CreateAppointmentHandler> logger, IEmailSender emailSender) : INotificationHandler<CreateAppointmentEvent>
{
    public async Task Handle(CreateAppointmentEvent notification, CancellationToken cancellationToken)
    {
        Appointment appointment = notification.Appointment;
        Doctor doctor = appointment.Doctor;
        Patient patient = appointment.Patient;

        logger.LogInformation("Handling Appointment creation {AppointmentId}", appointment.Id);

        var emailBody = new StringBuilder();

        emailBody.AppendLine($"Olá, Dr. {doctor.Name}!");
        emailBody.AppendLine($"Você tem uma nova consulta marcada!");
        emailBody.AppendLine($"Paciente: {patient.Name}.");
        emailBody.AppendLine($"Data e horário: {appointment.Start:dd/MM/yyyy} às {appointment.Start:HH:mm}.");

        await emailSender
            .SendEmailAsync(doctor.User.Email!,
                            "noreply@healthmed.com",
                            "Health&Med - Nova consulta agendada",
                            emailBody.ToString());
    }
}
