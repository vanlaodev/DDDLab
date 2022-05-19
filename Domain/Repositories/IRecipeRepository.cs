using Domain.Models;

namespace Domain.Repositories;

public interface IRecipeRepository : IRepository<Recipe>
{
    Task<Recipe?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<Recipe>> GetAllAsync(CancellationToken cancellationToken = default);

    Task AddAsync(Recipe recipe, CancellationToken cancellationToken = default);

    Task UpdateAsync(Recipe recipe, CancellationToken cancellationToken = default);
}