using OrderApp.Endpoints.Clients;

namespace OrderApp.Endpoints.Employees;

public class ClientGet
{
    public static string Template => "/clients";

    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };

    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static async Task<IResult> Action(HttpContext httpContext)
    {
        var userIdAuthenticated = httpContext.User;

        var result = new
        {
            Id = userIdAuthenticated.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value,
            Name = userIdAuthenticated.Claims.First(c => c.Type == "Name").Value,
            Cpf = userIdAuthenticated.Claims.First(c => c.Type == "Cpf").Value,
        };

        return Results.Ok(result);
    }
}