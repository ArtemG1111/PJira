

using Microsoft.Extensions.DependencyInjection;
using PJira.Application.Common.Mappers;
using System.Reflection;

namespace PJira.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(conf =>
            {
                conf.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });
            services.AddAutoMapper(typeof(AssignmentMappingProfile));

            return services;
        }
    }
}
