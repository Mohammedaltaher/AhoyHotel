using Application.Common.Mapper;
using Application.Filters;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;
public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        AssemblyScanner.FindValidatorsInAssembly(Assembly.GetExecutingAssembly())
          .ForEach(item => services.AddScoped(item.InterfaceType, item.ValidatorType));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationPipelineBehaviorFilter<,>));

        var mapperConfig = new MapperConfiguration(profile =>
        {
            profile.AddProfile(new HotelProfile());
            profile.AddProfile(new LookupProfile());
        });
        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);
        services.AddElasticsearch(configuration);
    }
}

