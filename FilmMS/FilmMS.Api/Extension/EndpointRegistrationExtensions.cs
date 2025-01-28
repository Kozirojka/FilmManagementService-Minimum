using FilmMS.Api.Interfaces;

namespace FilmMS.Api.Extension;

public static class EndpointRegistrationExtensions
{
    public static void RegisterAllEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var endpointDefinitions = AppDomain
            .CurrentDomain
            .GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(type => typeof(IEndpoint).IsAssignableFrom(type)
                && !type.IsInterface && !type.IsAbstract)
            .Select(Activator.CreateInstance)
            .Cast<IEndpoint>();
        
        foreach (var endpoint in endpointDefinitions)
        {
            endpoint.RegisterEndpoints(endpoints);
        }
    }
}