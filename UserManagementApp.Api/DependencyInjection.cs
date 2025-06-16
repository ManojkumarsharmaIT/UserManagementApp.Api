using UserManagementApp.Application;
using UserManagementApp.Infrastructure;

namespace UserManagementApp.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiDI(this IServiceCollection services,IConfiguration config)
        {
            services.AddApplicationDI()
                .AddInfrastructureDI(config);
            return services;
        }
    }
}
