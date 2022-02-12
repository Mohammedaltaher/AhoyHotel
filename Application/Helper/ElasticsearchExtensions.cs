using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;

namespace Application;
public static class ElasticsearchExtensions
{
    public static void AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = new ConnectionSettings(new Uri(configuration["elasticsearch:url"]))
            .BasicAuthentication(configuration["elasticsearch:username"], configuration["elasticsearch:password"])
            .CertificateFingerprint(configuration["elasticsearch:certificate"])
            .DefaultIndex(configuration["elasticsearch:index"]);

        AddDefaultMappings(settings);

        var client = new ElasticClient(settings);
        services.AddSingleton(client);

        CreateIndex(client, configuration["elasticsearch:index"]);
    }

    private static void AddDefaultMappings(ConnectionSettings settings)
    {
        settings.DefaultMappingFor<Hotel>(m => m.Ignore(p => p.UpdateDate));
        //settings.DefaultMappingFor<string>(m => m.Ignore(p => p.UpdateDate));
    }

    private static void CreateIndex(IElasticClient client, string indexName)
    {
        client.Indices.Create(indexName, index => index.Map<Hotel>(x => x.AutoMap()));
    }
}