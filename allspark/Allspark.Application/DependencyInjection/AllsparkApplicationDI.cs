namespace Allspark.Application.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class AllsparkApplicationDI
{
    public static IServiceCollection AddAllsparkApplicationDI(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(AllsparkApplicationDI).Assembly));
        return services;
    }
}
