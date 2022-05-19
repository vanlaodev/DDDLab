using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Tests;

public class RecipeRepositoryTests : TestBase
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly ILogger<RecipeRepositoryTests> _logger;

    public RecipeRepositoryTests()
    {
        _recipeRepository = Services.GetRequiredService<IRecipeRepository>();
        _logger = Services.GetRequiredService<ILogger<RecipeRepositoryTests>>();
    }

    [Fact]
    public async Task GetAllRecipesTest()
    {
        await AddRecipeTest();
        var recipes = await _recipeRepository.GetAllAsync();
        Assert.NotEmpty(recipes);
    }

    [Fact]
    public async Task GetRecipeByIdTest()
    {
        var recipeToBeAdded = Recipe.Create("First recipe", Enumerable.Range(1, 10).Select(x => $"Recipe step {x}"));
        await _recipeRepository.AddAsync(recipeToBeAdded);
        var recipe = await _recipeRepository.GetByIdAsync(recipeToBeAdded.Id);
        Assert.Equal(recipeToBeAdded, recipe);
    }

    [Fact]
    public async Task AddRecipeTest()
    {
        var recipe = Recipe.Create("First recipe", Enumerable.Range(1, 10).Select(x => $"Recipe step {x}"));
        await _recipeRepository.AddAsync(recipe);
    }

    [Fact]
    public async Task UpdateRecipeTest()
    {
        var recipeToBeAdded = Recipe.Create("First recipe", Enumerable.Range(1, 10).Select(x => $"Recipe step {x}"));
        await _recipeRepository.AddAsync(recipeToBeAdded);
        var recipe = await _recipeRepository.GetByIdAsync(recipeToBeAdded.Id) ?? throw new Exception("Recipe not found.");
        recipe.UpdateDescription("Updated recipe");
        recipe.Steps.ElementAt(0).UpdateDescription($"{recipe.Steps.ElementAt(0).Description} (updated 1)"); ;
        recipe.Steps.ElementAt(1).UpdateDescription($"{recipe.Steps.ElementAt(1).Description} (updated 2)"); ;
        recipe.UpdateSteps(new List<RecipeStep>(recipe.Steps) { RecipeStep.Create("New step") });
        await _recipeRepository.UpdateAsync(recipe);
        var updatedRecipe = await _recipeRepository.GetByIdAsync(recipeToBeAdded.Id) ?? throw new Exception("Recipe not found.");
        Assert.Equal(recipe.Description, updatedRecipe.Description);
    }
}