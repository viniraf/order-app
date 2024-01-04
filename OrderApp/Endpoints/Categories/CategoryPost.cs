using OrderApp.Domain.Products;
using OrderApp.Infra.Data;

namespace OrderApp.Endpoints.Categories;

public class CategoryPost
{
    public static string Template => "/categories";

    public static string[] Methods => new string[] {HttpMethod.Post.ToString()};

    public static Delegate Handle => Action;

    public static IResult Action (CategoryRequest categoryRequest, ApplicationDbContext context)
    {
        var category = new Category
        {
            Name = categoryRequest.Name,
            CreatedBy = "AdminTest",
            CreatedOn = DateTime.Now,
            EditedBy = "AdminTest",
            EditedOn = DateTime.Now,
        };
        context.Categories.Add(category);
        context.SaveChanges();

        return Results.Created($"/categories/{category.Id}", $"Created Category Id: {category.Id}");
    }
}
