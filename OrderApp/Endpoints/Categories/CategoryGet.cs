using OrderApp.Domain.Products;
using OrderApp.Infra.Data;

namespace OrderApp.Endpoints.Categories;

public class CategoryGet
{
    public static string Template => "/categories";

    public static string[] Methods => new string[] {HttpMethod.Get.ToString()};

    public static Delegate Handle => Action;

    public static IResult Action (ApplicationDbContext context)
    {
        var categories = context.Categories.ToList();

        var categoriesResponse = categories.Select(c => new CategoryResponse
        {
            Id = c.Id,
            Name = c.Name,
            Active = c.Active,
        });

        return Results.Ok(categoriesResponse);
    }
}
