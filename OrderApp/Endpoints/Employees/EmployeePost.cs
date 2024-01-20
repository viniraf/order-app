﻿using Flunt.Notifications;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize(Policy = "EmployeePolicy")]
    public static IResult Action(EmployeeRequest employeeRequest, HttpContext httpContext, UserManager<IdentityUser> userManager)
    {
        var userIdAuthenticated = httpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

        var user = new IdentityUser { UserName = employeeRequest.Email, Email = employeeRequest.Email };

        var result = userManager.CreateAsync(user, employeeRequest.Password).Result;

        if (!result.Succeeded)
        {
            return Results.ValidationProblem(result.Errors.ConvertToProblemDetails());
        }

        var userClaims = new List<Claim>
        {
            new Claim("EmployeeCode", employeeRequest.EmployeeCode),
            new Claim("Name", employeeRequest.Name),
            new Claim("CreatedBy", userIdAuthenticated),
        };

        var claimResult = userManager.AddClaimsAsync(user, userClaims).Result;

        if (!claimResult.Succeeded)
        {
            return Results.ValidationProblem(claimResult.Errors.ConvertToProblemDetails());
        }

        return Results.Created($"/employees/{user.Id}", $"Created Employee Id: {user.Id}");
    }
}