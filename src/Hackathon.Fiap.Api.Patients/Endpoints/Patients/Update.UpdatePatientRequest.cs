﻿using Microsoft.AspNetCore.Mvc;

namespace Hackathon.Fiap.Api.Patients.Endpoints.Patients;

public record UpdatePatientRequest(string Name)
{
    public const string Route = "/patients/{Id:int}";

    [FromRoute]
    public int Id { get; set; }
}

