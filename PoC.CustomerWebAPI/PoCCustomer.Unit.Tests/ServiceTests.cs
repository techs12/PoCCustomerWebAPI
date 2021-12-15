using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using PocCustomer.Model;
using PoCCustomer.Repository;
using PoCCustomer.Service;

namespace PoCCustomer.Unit.Tests
{
    public class ServiceTests
    {
        private Mock<ICustomerRepository> _repoMock;

        public ServiceTests()
        {
            _repoMock = new Mock<ICustomerRepository>();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void AddCustomer(bool customerExists)
        {
            var c = new Customer();
            _repoMock.Setup(repo => repo.AddEditCustomer(c)).Returns(c);
            _repoMock.Setup(repo => repo.GetCustomer_FirstLastName(c)).Returns(customerExists);

            var service = new CustomerService(_repoMock.Object);
            var res = service.AddEditCustomer(c);

            if (customerExists)
                Assert.Null(res.Customer);
            else
                Assert.NotNull(res.Customer);

            Assert.Equal(customerExists, res.Exists);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void EditCustomer(bool customerExists)
        {
            var c = new Customer();
            _repoMock.Setup(repo => repo.AddEditCustomer(c)).Returns(customerExists ? c : null);
            _repoMock.Setup(repo => repo.GetCustomer_Id(0)).Returns(customerExists ? c : null);

            var service = new CustomerService(_repoMock.Object);
            var res = service.AddEditCustomer(c, true);

            if (customerExists)
                Assert.NotNull(res.Customer);
            else
                Assert.Null(res.Customer);

            Assert.Equal(customerExists, res.Exists);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void DeleteCustomer(bool customerExists)
        {
            var c = new Customer();
            _repoMock.Setup(repo => repo.DeleteCustomer(c)).Returns(c);
            _repoMock.Setup(repo => repo.GetCustomer_Id(1)).Returns(customerExists ? c : null);

            var service = new CustomerService(_repoMock.Object);
            var res = service.DeleteCustomer_Id(1);

            if (customerExists)
                Assert.NotNull(res.Customer);
            else
                Assert.Null(res.Customer);

            Assert.Equal(customerExists, res.Exists);
        }

        [Theory]
        [InlineData("")]
        [InlineData("test")]
        public void FindCustomer(string criteria)
        {
            var lc = new List<Customer>();
            _repoMock.Setup(repo => repo.FindCustomers(criteria)).Returns(lc);

            var service = new CustomerService(_repoMock.Object);
            var res = service.FindCustomer_FirstAndLastName(criteria);

            if (string.IsNullOrEmpty(criteria))
            {
                Assert.Null(res.Customer);
                Assert.False(res.Exists);
            }
            else
            {
                Assert.NotNull(res.Customer);
                Assert.True(res.Exists);
            }

        }

    }
}
