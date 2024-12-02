using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services;
    }
}