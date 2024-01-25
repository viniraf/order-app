﻿namespace OrderApp.Endpoints.Clients;

public class ClientRequest
{
    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Cpf { get; set; } = string.Empty;
}