using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using PocCustomer.Model;
using PoCCustomer.Repository;

namespace PoCCustomer.Unit.Tests
{
    public class RepositoryTests : PoCCustomerDbFixture
    {
        [Fact]
        public void GetAllCustomers()
        {
            AddTestCustomers();
            var repo = new CustomerRepository(Context);
            var customers = repo.GetAllCustomers();

            Assert.NotNull(customers);
            Assert.Equal(10, customers.Count());
        }

        [Fact]
        public void DeleteCustomer()
        {
            var addedCustomers = AddTestCustomers();
            var repo = new CustomerRepository(Context);
            var customerToDelete = addedCustomers.FirstOrDefault(c => c.FirstName == "wood-5");
            var customer = repo.DeleteCustomer(customerToDelete);

            Assert.Equal(customerToDelete.Id, customer.Id);
            Assert.Equal(9, repo.GetAllCustomers().Count());
        }

        [Fact]
        public void DeleteCustomer_NotFound()
        {
            AddTestCustomers();
            var repo = new CustomerRepository(Context);
            var customer = repo.DeleteCustomer(new Customer { Id = int.MaxValue });

            Assert.Null(customer);
            Assert.Equal(10, repo.GetAllCustomers().Count());
        }

        [Fact]
        public void AddCustomer()
        {
            var repo = new CustomerRepository(Context);
            var customer = repo.AddEditCustomer(new Customer
            { FirstName = "wood", LastName = "forest", DateOfBirth = new DateTime() }
            );

            Assert.Single(repo.GetAllCustomers());
        }

        [Fact]
        public void EditCustomer()
        {
            AddTestCustomers();
            var repo = new CustomerRepository(Context);
            var editCustomer = repo.GetAllCustomers().FirstOrDefault(c => c.FirstName == "wood-4");
            var editCustomerId = editCustomer.Id;
            var now = DateTime.Now;

            editCustomer.FirstName = "wood";
            editCustomer.LastName = "forest";
            editCustomer.DateOfBirth = now;
            var customer = repo.AddEditCustomer(editCustomer);

            Assert.Equal(customer, repo.GetCustomer_Id(editCustomerId));
        }

        [Fact]
        public void FindCustomers()
        {
            AddTestCustomers();
            var repo = new CustomerRepository(Context);

            var customers = repo.FindCustomers("wood");
            Assert.Equal(10, customers.Count());

            customers = repo.FindCustomers("forest");
            Assert.Equal(10, customers.Count());

            customers = repo.FindCustomers("wood-5");
            Assert.Single(customers);
        }

        private IEnumerable<Customer> AddTestCustomers()
        {
            for (int i = 0; i < 10; i++)
            {
                var customer = new Customer { FirstName = $"wood-{i}", LastName = $"forest-{i}", DateOfBirth = new DateTime().AddYears(i) };
                Context.Customers.Add(customer);
            }

            Context.SaveChanges();

            return Context.Customers.AsEnumerable();
        }
    }
}
