using Microsoft.Extensions.DependencyInjection;
using PoCCustomer.Repository;
using PoCCustomer.Service;

namespace PoC.CustomerWebAPI.Extensions.Startup
{
    public static class DependencyInjectionConfig
    {
        public static void AddAppServiceConfigurations(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();
        }
    }
}
