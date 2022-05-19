namespace Domain.Models;

public class Recipe : EntityBase<Guid>, IAggregateRoot
{
    public string? Description { get; private set; }
    public IEnumerable<RecipeStep> Steps { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public static Recipe Create(string? description, IEnumerable<string> stepDescriptions)
    {
        return new Recipe(Guid.NewGuid(), description, (stepDescriptions ?? new string[0]).Select(RecipeStep.Create).ToList(), DateTime.Now, null);
    }

    public Recipe(Guid id, string? description, IEnumerable<RecipeStep> steps, DateTime createdAt, DateTime? updatedAt) : base(id)
    {
        Description = description;
        Steps = steps ?? new RecipeStep[0];
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public Recipe UpdateSteps(IEnumerable<RecipeStep> steps)
    {
        Steps = steps ?? new RecipeStep[0];
        UpdatedAt = DateTime.Now;
        return this;
    }

    public Recipe UpdateDescription(string? description)
    {
        Description = description;
        UpdatedAt = DateTime.Now;
        return this;
    }
}