namespace OrderApp.Domain.Products;

public class Product : Entity
{
    public string Name { get; set; } = string.Empty;

    public Guid CategoryId { get; set; }

    public string Description { get; set; } = string.Empty;

    public Category? Category { get; set; } 

    public bool HasStock { get; set; }

    public bool Active { get; set; }

    public Product(string name, Category category, string description, bool hasTock, string createdBy)
    {
        Name = name;
        Category = category;
        Description = description;
        HasStock = hasTock;
        CreatedBy = createdBy;
        EditedBy = createdBy;
        CreatedOn = DateTime.Now;
        EditedOn = DateTime.Now;
        Active = true;

        var contract = new Contract<Category>()
            .IsNotNullOrEmpty(name, "Name")
            .IsGreaterOrEqualsThan(name, 3, "Name")
            .IsNotNull(category, "Category")
            .IsNotNullOrEmpty(description, "Description")
            .IsGreaterOrEqualsThan(description, 3, "Description")
            .IsNotNullOrEmpty(createdBy, "CreatedBy")
            .IsNotNullOrEmpty(createdBy, "EditedBy");
        AddNotifications(contract);


    }
}
