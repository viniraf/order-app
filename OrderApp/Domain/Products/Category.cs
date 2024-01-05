using Flunt.Notifications;
using Flunt.Validations;

namespace OrderApp.Domain.Products;

public class Category : Entity
{
    public string Name { get; set; } = string.Empty;

    public bool Active { get; set; } = true;

    public Category(string name, string createdBy, string editedBy)
    {
        var contract = new Contract<Category>()
            .IsNotNullOrEmpty(name, "Name")
            .IsNotNullOrEmpty(createdBy, "CreatedBy")
            .IsNotNullOrEmpty(editedBy, "EditedBy");
        AddNotifications(contract);

        Name = name;
        Active = true;
        CreatedBy = createdBy;
        EditedBy = editedBy;
        CreatedOn = DateTime.Now;
        EditedOn = DateTime.Now;
    }

}
