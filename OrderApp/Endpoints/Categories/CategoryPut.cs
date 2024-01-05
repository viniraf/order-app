using Microsoft.AspNetCore.Mvc;
using OrderApp.Domain.Products;
using OrderApp.Infra.Data;

namespace OrderApp.Endpoints.Categories;

public class CategoryPut
{
    public static string Template => "/categories/{id:guid}";

    public static string[] Methods => new string[] {HttpMethod.Put.ToString()};

    public static Delegate Handle => Action;

    public static IResult Action ([FromRoute] Guid id, CategoryRequest categoryRequest, ApplicationDbContext context)
    {
        var category = context.Categories.Where(c => c.Id == id).FirstOrDefault();

        if (category == null)
        {
            return Results.NotFound("This category not exists");
        }

        category.Name = categoryRequest.Name;
        category.Active = categoryRequest.Active;
        category.EditedBy = "AdminTestAlter";
        category.EditedOn = DateTime.Now;

        context.SaveChanges();

        return Results.Ok("Category has been update");
    }
}
