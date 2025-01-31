using Hackathon.Fiap.Core.Aggregates.Appointments.Events;
using Hackathon.Fiap.Core.Aggregates.Doctors;
using Hackathon.Fiap.Core.Interfaces;

namespace Hackathon.Fiap.Core.Aggregates.Appointments.Handlers;

internal class CreateAppointmentHandler(ILogger<CreateAppointmentHandler> logger, IEmailSender emailSender) : INotificationHandler<CreateAppointmentEvent>
{
    public async Task Handle(CreateAppointmentEvent notification, CancellationToken cancellationToken)
    {
        Appointment appointment = notification.Appointment;
        Doctor doctor = notification.Doctor;

        logger.LogInformation("Handling Appointment creation {AppointmentId}", appointment.Id);

        await emailSender
            .SendEmailAsync(doctor.User.Email,
                            "noreply@healthmed.com",
                            "Health&Med - Nova consulta agendada",
                            $"Olá, Dr. {doctor.Name}!\r\nVocê tem uma nova consulta marcada! Paciente: {{nome_do_paciente}}.\r\nData e horário: {appointment.Start:dd/MM/yyyy}\r\nàs {appointment.Start:HH:mm}.");
    }
}
