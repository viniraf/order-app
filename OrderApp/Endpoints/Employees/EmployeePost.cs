using Flunt.Notifications;
using Microsoft.AspNetCore.Identity;
using OrderApp.Domain.Products;
using OrderApp.Infra.Data;

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
            // TODO: Add problem details for result
            return Results.BadRequest(result.Errors.First());
        }

        return Results.Created($"/employees/{user.Id}", $"Created Employee Id: {user.Id}");
    }
}