using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace PoC.CustomerWebAPI.Extensions.Startup
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfigurations(this IServiceCollection services)
        {
            services.AddSwaggerGen(sg =>
            {
                sg.SwaggerDoc("v1", new OpenApiInfo { Title = "PoC Customer Coding Test", Version = "v1" });
            });
        }
    }
}
