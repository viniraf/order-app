namespace OrderApp.Domain.Products;

public class Product : Entity
{
    public string Name { get; set; } = string.Empty;

    public int CategoryId { get; set; }

    public string Description { get; set; } = string.Empty;

    public Category? Category { get; set; } 

    public bool HasStock { get; set; }

    public bool Active { get; set; }
}
