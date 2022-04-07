using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace Application.Helper;
public static class ElasticSearchExtensions
{
    public static void AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = new ConnectionSettings(new Uri(configuration["elastic-search:url"]))
            .BasicAuthentication(configuration["elastic-search:username"], configuration["elastic-search:password"])
            .CertificateFingerprint(configuration["elastic-search:certificate"])
            .DefaultIndex(configuration["elastic-search:index"]);

        AddDefaultMappings(settings);

        ElasticClient client = new(settings);
        services.AddSingleton(client);

        CreateIndex(client, configuration["elastic-search:index"]);
    }

    private static void AddDefaultMappings(ConnectionSettings settings)
    {
        settings.DefaultMappingFor<Hotel>(m => m.Ignore(p => p.UpdateDate));
    }

    private static void CreateIndex(IElasticClient client, string indexName)
    {
        client.Indices.Create(indexName, index => index.Map<Hotel>(x => x.AutoMap()));
    }
}