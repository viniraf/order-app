using Flunt.Notifications;
using Flunt.Validations;

namespace OrderApp.Domain.Products;

public class Category : Entity
{
    public string Name { get; set; } = string.Empty;

    public bool Active { get; set; } = true;

    public Category(string name)
    {
        var contract = new Contract<Category>()
            .IsNotNullOrEmpty(name, "Name");

        AddNotifications(contract);

        Name = name;
        Active = true;
    }

}
