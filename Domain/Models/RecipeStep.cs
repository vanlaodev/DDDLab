namespace Domain.Models;

public class RecipeStep : EntityBase<Guid>
{
    public string? Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public static RecipeStep Create(string? description)
    {
        return new RecipeStep(Guid.NewGuid(), description, DateTime.Now, null);
    }

    public RecipeStep(Guid id, string? description, DateTime createdAt, DateTime? updatedAt) : base(id)
    {
        Description = description;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public RecipeStep UpdateDescription(string? description)
    {
        Description = description;
        UpdatedAt = DateTime.Now;
        return this;
    }
}