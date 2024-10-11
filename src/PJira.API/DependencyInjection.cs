

namespace PJira.API
{
    public static class DependencyInjection
    {
        public static WebApplicationBuilder AddAPIServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();

            builder.Services.AddSwaggerGen();

            return builder;
        }
    }
}
