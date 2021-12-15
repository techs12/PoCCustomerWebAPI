using PocCustomer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoCCustomer.Unit.Tests
{
    public class PoCCustomerDbFixture : IDisposable
    {
        public PoCCustomerContext Context { get; private set; }

        public PoCCustomerDbFixture()
        {
            var options = new DbContextOptionsBuilder<PoCCustomerContext>().UseInMemoryDatabase("alinta-test").Options;
            Context = new PoCCustomerContext(options);
            Context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
