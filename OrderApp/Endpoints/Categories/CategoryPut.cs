﻿namespace OrderApp.Endpoints.Categories;

public class CategoryPut
{
    public static string Template => "/categories/{id:guid}";

    public static string[] Methods => new string[] {HttpMethod.Put.ToString()};

    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static IResult Action ([FromRoute] Guid id, CategoryRequest categoryRequest, HttpContext httpContext, ApplicationDbContext context)
    {
        var userId = httpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var category = context.Categories.Where(c => c.Id == id).FirstOrDefault();

        if (category == null)
        {
            return Results.NotFound("This category not exists");
        }

        category.Name = categoryRequest.Name;
        category.Active = categoryRequest.Active;
        category.EditedBy = userId;
        category.EditedOn = DateTime.Now;

        if (!category.IsValid)
        {
            return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails());
        }

        context.SaveChanges();

        return Results.Ok("Category has been update");
    }
}
