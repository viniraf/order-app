namespace OrderApp.Endpoints.Employees;

public class EmployeeGet
{
    public static string Template => "/employees";

    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };

    public static Delegate Handle => Action;

    [Authorize(Policy ="EmployeePolicy")]
    public static async Task<IResult> Action(QueryAllUsersWithClaimName query, [FromQuery] int page = 1, [FromQuery] int rows = 10)
    {
        var employees = await query.Execute(page, rows);

        if (employees is null || employees.Any() == false)
        {
            return Results.NotFound("There is no record in the database");
        }
        
        return Results.Ok(employees);
    }
}