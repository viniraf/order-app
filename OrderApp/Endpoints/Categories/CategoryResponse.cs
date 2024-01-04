namespace OrderApp.Endpoints.Categories;

public class CategoryResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public bool Active { get; set; }
}
