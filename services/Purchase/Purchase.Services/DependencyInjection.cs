using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Purchase.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
