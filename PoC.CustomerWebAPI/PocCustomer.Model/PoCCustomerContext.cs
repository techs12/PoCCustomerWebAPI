using Microsoft.EntityFrameworkCore;

namespace PocCustomer.Model
{
    public class PoCCustomerContext : DbContext
    {
        public PoCCustomerContext(DbContextOptions<PoCCustomerContext> options)
           : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
