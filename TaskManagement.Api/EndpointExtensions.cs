using System.Reflection;
using TaskManagement.Application;

namespace TaskManagement.Api;

public static class EndpointExtensions
{
    public static IServiceCollection AddEndpoints(this IServiceCollection service, Assembly assembly)
    {
        IEnumerable<TypeInfo> descriptor =
            assembly.DefinedTypes
                .Where(d => !d.IsAbstract && !d.IsInterface && d.IsClass && d.IsAssignableTo(typeof(IEndpoints)));

        foreach (TypeInfo descriptors in descriptor)
        {
            service.AddTransient(typeof(IEndpoints), descriptors);
        }
        
        return service;
    }
    
    public static IApplicationBuilder UseMapEndpoints(this WebApplication app, CancellationToken cancellationToken,
        RouteGroupBuilder? routeGroupBuilder = null)
    {
        IEnumerable<IEndpoints> endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoints>>();
        
        IEndpointRouteBuilder builder;
        if (routeGroupBuilder is not null) builder = routeGroupBuilder;
        else builder = app;
        
        foreach (IEndpoints endpoint in endpoints)
            endpoint.Map(builder, cancellationToken);
        
        return app;
    }
}