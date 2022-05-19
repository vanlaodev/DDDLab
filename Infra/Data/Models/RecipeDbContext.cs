using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Models;

public class RecipeDbContext : DbContext
{
    public RecipeDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Recipe> Recipes { get; set; } = default!;
    public DbSet<RecipeStep> RecipeSteps { get; set; } = default!;
}