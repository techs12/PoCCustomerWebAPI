using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PocCustomer.Model;

namespace PoC.CustomerWebAPI.Extensions.Startup
{
    public static class InMemoryDbConfig
    {
        public static void AddDbConextConfigurations(this IServiceCollection services)
        {
            services.AddDbContext<PoCCustomerContext>(opt => opt.UseInMemoryDatabase("PoCCustomer"));
        }
    }
}
