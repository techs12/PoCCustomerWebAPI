using System;
using System.Collections.Generic;
using System.Linq;
using PocCustomer.Model;

namespace PoCCustomer.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly PoCCustomerContext _context;
        public CustomerRepository(PoCCustomerContext context)
        {
            _context = context;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _context.Customers;
        }

        public Customer GetCustomer_Id(int id)
        {
            return _context.Customers.FirstOrDefault(c => c.Id == id);
        }

        public bool GetCustomer_FirstLastName(Customer customer)
        {
            return _context.Customers.Any(c => c.FirstName == customer.FirstName && c.LastName == customer.LastName);
        }

        public Customer DeleteCustomer(Customer customer)
        {
            var customerToDelete = _context.Customers.FirstOrDefault(c => c.Id == customer.Id);
            if (customerToDelete != null)
            {
                _context.Customers.Remove(customerToDelete);
                _context.SaveChanges();
            }

            return customerToDelete;
        }

        public Customer AddEditCustomer(Customer customer)
        {
            var res = _context.Customers.Update(customer);
            _context.SaveChanges();

            return res.Entity;
        }

        public IEnumerable<Customer> FindCustomers(string searchCriteria)
        {
            var res = _context.Customers.Where(c =>
                c.FirstName.Contains(searchCriteria, StringComparison.OrdinalIgnoreCase) ||
                c.LastName.Contains(searchCriteria, StringComparison.OrdinalIgnoreCase))
                .AsEnumerable();

            return res;
        }
    }
}
