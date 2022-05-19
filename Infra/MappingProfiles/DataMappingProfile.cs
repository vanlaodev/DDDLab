using AutoMapper;

namespace Infra.MappingProfiles;

public class DataMappingProfile : Profile
{
    public DataMappingProfile()
    {
        CreateMap<Domain.Models.RecipeStep, Data.Models.RecipeStep>().ReverseMap();

        CreateMap<Domain.Models.Recipe, Data.Models.Recipe>()
            .AfterMap((s, d) =>
            {
                d.Steps.ForEach(x => x.RecipeId = d.Id);
            })
            .ReverseMap();
    }
}