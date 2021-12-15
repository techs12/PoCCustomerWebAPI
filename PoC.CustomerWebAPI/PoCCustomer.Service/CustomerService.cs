using PocCustomer.Model;
using PoCCustomer.Repository;
using System.Collections.Generic;

namespace PoCCustomer.Service
{
    public class CustomerService : ICustomerService
    {
        public ICustomerRepository _repository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _repository = customerRepository;
        }

        public Response<Customer> AddEditCustomer(Customer customer, bool edit = false)
        {
            var res = new Response<Customer>();
            if (edit)
            {
                var c = _repository.GetCustomer_Id(customer.Id);
                if (c == null)
                {
                    res.Customer = c;
                    res.Exists = false;
                }
                else
                {
                    // ideally this would go into a mapper class
                    c.FirstName = customer.FirstName;
                    c.LastName = customer.LastName;
                    c.DateOfBirth = customer.DateOfBirth;
                    res.Customer = _repository.AddEditCustomer(c);
                    res.Exists = true;
                }
            }
            else
            {
                var exists = _repository.GetCustomer_FirstLastName(customer);
                var c = exists ? null : _repository.AddEditCustomer(customer);

                res.Customer = c;
                res.Exists = exists;
            }

            return res;
        }

        public Response<Customer> DeleteCustomer_Id(int id)
        {
            var customerToDelete = _repository.GetCustomer_Id(id);
            var exists = customerToDelete != null;
            if (exists)
                customerToDelete = _repository.DeleteCustomer(customerToDelete);

            return new Response<Customer> { Customer = customerToDelete, Exists = exists };
        }

        public Response<IEnumerable<Customer>> FindCustomer_FirstAndLastName(string criteria)
        {
            var res = new Response<IEnumerable<Customer>> { Exists = true };
            if (string.IsNullOrEmpty(criteria?.Trim()))
                res.Exists = false;
            else
                res.Customer = _repository.FindCustomers(criteria);

            return res;
        }
    }
}

