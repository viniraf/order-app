using Flunt.Notifications;
using Microsoft.AspNetCore.Authorization;
using OrderApp.Domain.Products;
using OrderApp.Infra.Data;

namespace OrderApp.Endpoints.Categories;

public class CategoryPost
{
    public static string Template => "/categories";

    public static string[] Methods => new string[] {HttpMethod.Post.ToString()};

    public static Delegate Handle => Action;

    [Authorize]
    public static IResult Action (CategoryRequest categoryRequest, ApplicationDbContext context)
    {
        var createdByTemp = "AdminTest";
        var editedByTemp = "AdminTest";

        var category = new Category(categoryRequest.Name, createdByTemp, editedByTemp);

        if (!category.IsValid)
        {
            return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails());
        }

        context.Categories.Add(category);
        context.SaveChanges();

        return Results.Created($"/categories/{category.Id}", $"Created Category Id: {category.Id}");
    }
}
