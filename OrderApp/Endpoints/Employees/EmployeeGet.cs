using Flunt.Notifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrderApp.Domain.Products;
using OrderApp.Infra.Data;
using System.Security.Claims;
using DotNetEnv;
using Microsoft.Data.SqlClient;
using Dapper;

namespace OrderApp.Endpoints.Employees;

public class EmployeeGet
{
    public static string Template => "/employees";

    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };

    public static Delegate Handle => Action;

    public static IResult Action([FromQuery] int page = 1, [FromQuery] int rows = 10)
    {
        string connectionString = Env.GetString("DB_CONNECTION_STRING");
        var db = new SqlConnection(connectionString);

        string query =
            @"SELECT 
	            C.ClaimValue [Name],
	            U.Email
            FROM [OrderApp].[dbo].[AspNetUsers] U
            INNER JOIN [OrderApp].[dbo].[AspNetUserClaims] C ON U.Id = C.UserId
            WHERE ClaimType = 'Name'
            ORDER BY C.ClaimValue
            OFFSET (@page -1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";

        var employees = db.Query<EmployeeResponse>(query, new { page, rows});

        
        return Results.Ok(employees);
    }
}