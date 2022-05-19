using System.ComponentModel.DataAnnotations;

namespace Infra.Data.Models;

public class Recipe
{
    [Key]
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public List<RecipeStep> Steps { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}