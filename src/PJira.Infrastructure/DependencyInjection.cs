

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PJira.Application.Common.Interfaces;
using PJira.Infrastructure.Data;

namespace PJira.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services
            ,IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PJiraSqlDB");

            services.AddDbContext<PJiraDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IPJiraDbContext, PJiraDbContext>();

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = true;

            })
                .AddEntityFrameworkStores<PJiraDbContext>();


            return services;
        }
    }
}
