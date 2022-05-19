using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Data.Models;

public class RecipeStep
{
    [Key]
    public Guid Id { get; set; }
    public Guid RecipeId { get; set; }
    [ForeignKey(nameof(RecipeId))]
    public Recipe Recipe { get; set; } = default!;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}