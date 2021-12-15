using PocCustomer.Model;
using System.Collections.Generic;

namespace PoCCustomer.Repository
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomer_Id(int id);
        bool GetCustomer_FirstLastName(Customer customer);
        Customer DeleteCustomer(Customer customer);
        Customer AddEditCustomer(Customer customer);
        IEnumerable<Customer> FindCustomers(string searchCriteria);
    }
}
