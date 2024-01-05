using Flunt.Notifications;
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
        var createdByTemp = "AdminTest";
        var editedByTemp = "AdminTest";

        var category = new Category(categoryRequest.Name, createdByTemp, editedByTemp);

        if (!category.IsValid)
        {
            var erros = category.Notifications
                .GroupBy(c => c.Key)
                .ToDictionary(c => c.Key, g => g.Select(x => x.Message).ToArray());

            return Results.ValidationProblem(erros);
        }

        context.Categories.Add(category);
        context.SaveChanges();

        return Results.Created($"/categories/{category.Id}", $"Created Category Id: {category.Id}");
    }
}
