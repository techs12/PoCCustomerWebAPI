using PocCustomer.Model;
using System.Collections.Generic;

namespace PoCCustomer.Service
{
    public interface ICustomerService
    {
        Response<Customer> AddEditCustomer(Customer customer, bool edit = false);
        Response<Customer> DeleteCustomer_Id(int id);
        Response<IEnumerable<Customer>> FindCustomer_FirstAndLastName(string criteria);
    }
}
