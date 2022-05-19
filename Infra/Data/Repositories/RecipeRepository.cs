using AutoMapper;
using Domain.Repositories;
using Infra.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories;

public class RecipeRepository : IRecipeRepository
{
    private readonly RecipeDbContext _dbCtx;
    private readonly IMapper _mapper;

    public RecipeRepository(RecipeDbContext dbContext, IMapper mapper)
    {
        _dbCtx = dbContext;
        _mapper = mapper;
    }

    public async Task AddAsync(Domain.Models.Recipe recipe, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(recipe);
        var dm = _mapper.Map<Data.Models.Recipe>(recipe);
        await _dbCtx.Recipes.AddAsync(dm, cancellationToken);
        await _dbCtx.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Domain.Models.Recipe>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var dms = await _dbCtx.Recipes.Include(x => x.Steps).AsNoTracking().ToListAsync(cancellationToken);
        return _mapper.Map<IEnumerable<Domain.Models.Recipe>>(dms);
    }

    public async Task<Domain.Models.Recipe?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var dm = await _dbCtx.Recipes.Include(x => x.Steps).AsNoTracking().Where(x => x.Id.Equals(id)).SingleOrDefaultAsync(cancellationToken);
        return dm == null ? null : _mapper.Map<Domain.Models.Recipe>(dm);
    }

    public async Task UpdateAsync(Domain.Models.Recipe recipe, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(recipe);
        var originalDm = await _dbCtx.Recipes.Include(x => x.Steps).AsTracking().Where(x => x.Id.Equals(recipe.Id)).SingleOrDefaultAsync(cancellationToken);
        if (originalDm == null) throw new Exception($"Recipe '{recipe.Id}' not found.");
        var dm = _mapper.Map<Data.Models.Recipe>(recipe);
        _dbCtx.Entry(originalDm).CurrentValues.SetValues(dm);
        _dbCtx.RecipeSteps.RemoveRange(originalDm.Steps.Where(s => !dm.Steps.Any(ss => ss.Id.Equals(s.Id))));
        await _dbCtx.RecipeSteps.AddRangeAsync(dm.Steps.Where(s => !originalDm.Steps.Any(ss => ss.Id.Equals(s.Id))));
        foreach (var step in originalDm.Steps.Where(s => dm.Steps.Any(ss => ss.Id.Equals(s.Id))))
        {
            _dbCtx.Entry(step).CurrentValues.SetValues(dm.Steps.Single(s => s.Id.Equals(step.Id)));
        }
        await _dbCtx.SaveChangesAsync(cancellationToken);
    }
}