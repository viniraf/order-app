namespace OrderApp.Domain;

public abstract class Entity
{
    public Entity()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }

    public string CreatedBy { get; set; } = string.Empty;

    public DateTime CreatedOn { get; set; }

    public string EditedBy { get; set; } = string.Empty;

    public DateTime EditedOn { get; set; }
}
