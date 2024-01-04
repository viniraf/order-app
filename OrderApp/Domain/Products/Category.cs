namespace OrderApp.Domain.Products;

public class Category : Entity
{
    public string Name { get; set; } = string.Empty;

    public bool Active { get; set; } = true;
}
