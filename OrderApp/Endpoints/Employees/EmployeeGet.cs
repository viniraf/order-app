using Flunt.Notifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrderApp.Domain.Products;
using OrderApp.Infra.Data;
using System.Security.Claims;

namespace OrderApp.Endpoints.Employees;

public class EmployeeGet
{
    public static string Template => "/employees";

    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };

    public static Delegate Handle => Action;

    public static IResult Action(UserManager<IdentityUser> userManager, [FromQuery] int page = 1, [FromQuery] int rows = 10)
    {                                                                                   
        var users = userManager.Users.Skip((page - 1) * rows).Take(rows).ToList();

        var employees = new List<EmployeeResponse>();

        foreach (var user in users)
        {
            var claims = userManager.GetClaimsAsync(user).Result;
            var claimName = claims.FirstOrDefault(c => c.Type == "Name");

            var userName = claimName is not null ? claimName.Value : string.Empty;

            employees.Add(new EmployeeResponse { Name = userName, Email = user.Email });
        }

        return Results.Ok(employees);
    }
}