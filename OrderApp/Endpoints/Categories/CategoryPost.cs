namespace OrderApp.Endpoints.Categories;

public class CategoryPost
{
    public static string Template => "/categories";

    public static string[] Methods => new string[] {HttpMethod.Post.ToString()};

    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action (CategoryRequest categoryRequest, HttpContext httpContext, ApplicationDbContext context)
    {
        var userId = httpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

        var createdByTemp = userId;
        var editedByTemp = userId;

        var category = new Category(categoryRequest.Name, createdByTemp, editedByTemp);

        if (!category.IsValid)
        {
            return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails());
        }

        await context.Categories.AddAsync(category);
        await context.SaveChangesAsync();

        return Results.Created($"/categories/{category.Id}", $"Created Category Id: {category.Id}");
    }
}
