namespace Allspark.Infrastructure.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class AllsparkInfrastructureDI
{
    public static IServiceCollection AddAllsparkInfrastructureDI(this IServiceCollection services)
    {
        // Register repositories and services using reflection
        var repositories = typeof(AllsparkInfrastructureDI).Assembly.GetTypes().Where(t => t.Name.EndsWith("Repository") && t is { IsClass: true, IsAbstract: false });
        foreach (var repository in repositories)
        {
            var interfaceType = repository.GetInterfaces().SingleOrDefault(i => i.Name.EndsWith(repository.Name));
            if (interfaceType != null)
            {
                services.AddTransient(interfaceType, repository);
                Console.WriteLine(interfaceType + " has been registered in the DI container: " + repository);
            }
        }
        return services;
    }
}