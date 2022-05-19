using System;
using Domain.Repositories;
using Infra.Data.Models;
using Infra.Data.Repositories;
using Infra.MappingProfiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Tests;

public abstract class TestBase
{
    protected readonly IServiceProvider Services;

    protected TestBase()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSqlServer<RecipeDbContext>(@"Server=10.0.8.99;Database=RecipeDb;User Id=sa;Password=p@ssw0rd;");
        serviceCollection.AddTransient<IRecipeRepository, RecipeRepository>();
        serviceCollection.AddAutoMapper(options =>
        {
            options.AddProfile<DataMappingProfile>();
        });
        serviceCollection.AddLogging(options =>
        {
            options.AddDebug();
        });
        Services = serviceCollection.BuildServiceProvider();
        var dbCtx = Services.GetRequiredService<RecipeDbContext>();
        dbCtx.Database.EnsureDeleted();
        dbCtx.Database.EnsureCreated();
    }
}