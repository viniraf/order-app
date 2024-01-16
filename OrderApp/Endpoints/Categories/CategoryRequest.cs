using System.ComponentModel.DataAnnotations;

namespace OrderApp.Endpoints.Categories;

public class CategoryRequest
{
    public string Name { get; set; } = string.Empty;

    public bool Active { get; set; }
}