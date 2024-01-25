﻿using OrderApp.Endpoints.Clients;

namespace OrderApp.Endpoints.Employees;

public class ClientPost
{
    public static string Template => "/clients";

    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };

    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static async Task<IResult> Action(ClientRequest clientRequest, HttpContext httpContext, UserManager<IdentityUser> userManager)
    {

        var newUser = new IdentityUser { UserName = clientRequest.Email, Email = clientRequest.Email };

        var result = await userManager.CreateAsync(newUser, clientRequest.Password);

        if (!result.Succeeded)
        {
            return Results.ValidationProblem(result.Errors.ConvertToProblemDetails());
        }

        var userClaims = new List<Claim>
        {
            new Claim("Cpf", clientRequest.Cpf),
            new Claim("Name", clientRequest.Name),
        };

        var claimResult = await userManager.AddClaimsAsync(newUser, userClaims);

        if (!claimResult.Succeeded)
        {
            return Results.ValidationProblem(claimResult.Errors.ConvertToProblemDetails());
        }

        return Results.Created($"/clients/{newUser.Id}", $"Created Client Id: {newUser.Id}");
    }
}