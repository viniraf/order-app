﻿using Flunt.Notifications;
using Microsoft.AspNetCore.Identity;
using OrderApp.Domain.Products;
using OrderApp.Infra.Data;
using System.Security.Claims;

namespace OrderApp.Endpoints.Employees;

public class EmployeePost
{
    public static string Template => "/employees";

    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };

    public static Delegate Handle => Action;

    public static IResult Action(EmployeeRequest employeeRequest, UserManager<IdentityUser> userManager)
    {
        var user = new IdentityUser { UserName = employeeRequest.Email, Email = employeeRequest.Email };

        var result = userManager.CreateAsync(user, employeeRequest.Password).Result;

        if (!result.Succeeded)
        {
            return Results.ValidationProblem(result.Errors.ConvertToProblemDetails());
        }

        var userClaims = new List<Claim>
        {
            new Claim("EmployeeCode", employeeRequest.EmployeeCode),
            new Claim("Name", employeeRequest.Name)
        };

        var claimResult = userManager.AddClaimsAsync(user, userClaims).Result;

        if (!claimResult.Succeeded)
        {
            return Results.ValidationProblem(claimResult.Errors.ConvertToProblemDetails());
        }

        return Results.Created($"/employees/{user.Id}", $"Created Employee Id: {user.Id}");
    }
}