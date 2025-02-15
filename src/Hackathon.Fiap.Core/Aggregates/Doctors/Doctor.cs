﻿using Hackathon.Fiap.Core.Aggregates.Doctors.Events;
using Hackathon.Fiap.Core.Aggregates.Users;

namespace Hackathon.Fiap.Core.Aggregates.Doctors;

public class Doctor(string name, string cpf, string crm) : EntityBase, IAggregateRoot
{
    public string Name { get; private set; } = name;
    public string Crm { get; private set; } = crm;
    public string Cpf { get; private set; } = cpf;

    public string UserId { get; set; } = default!;
    public ApplicationUser User { get; private set; } = default!;

    public int? SpecialtyId { get; set; }
    public Specialty? Specialty { get; private set; }

    public DoctorAppointmentConfiguration? AppointmentConfiguration { get; set; }

    public List<Schedule> Schedules { get; private set; } = [];

    public void AddSchedule(Schedule schedule)
    {
        Schedules.Add(schedule);
    }

    public void RemoveSchedule(Schedule schedule)
    {
        Schedules.Remove(schedule);
        RegisterDomainEvent(new RemoveScheduleEvent(this, schedule));
    }

    public void UpdateName(string newName)
    {
        Name = newName;
    }

    public void SetUser(ApplicationUser user)
    {
        User = user;
    }

    public void SetSpecialty(Specialty? specialty)
    {
        SpecialtyId = specialty?.Id;
        Specialty = specialty;
    }

    public void SetAppointmentConfiguration(DoctorAppointmentConfiguration? appointmentConfiguration)
    {
        AppointmentConfiguration = appointmentConfiguration;
    }

    public double GetAppointmentDuration()
    {
        return AppointmentConfiguration?.Duration ?? 0;
    }
}
