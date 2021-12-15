using PocCustomer.Model;
using PoCCustomer.Repository;
using PoCCustomer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit;
using PoC.CustomerWebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace PoCCustomer.Unit.Tests
{
    public class ControllerTests
    {
        private Mock<ICustomerService> _serviceMock;

        public ControllerTests()
        {
            _serviceMock = new Mock<ICustomerService>();
        }

        [Theory]
        [InlineData("wood", "forest", 1, 1, 1900)]
        [InlineData("", "forest", 1, 1, 1900)]
        [InlineData("wood", "", 1, 1, 1900)]
        [InlineData("wood", "forest", 1, 1, 2100)]
        public void AddCustomer(string first, string last, int day, int month, int year)
        {
            var c = new Customer
            {
                FirstName = first,
                LastName = last,
                DateOfBirth = new DateTime(year, month, day)
            };
            _serviceMock.Setup(s => s.AddEditCustomer(c, false)).Returns(new Response<Customer>());

            var controller = new CustomerController(_serviceMock.Object);
            var res = controller.AddCustomer(c);

            if (!c.IsValid())
                Assert.Equal(typeof(BadRequestObjectResult), res.Result.GetType());
            else
                Assert.Equal(typeof(CreatedResult), res.Result.GetType());
        }

        [Theory]
        [InlineData("wood", "forest", 1, 1, 1900)]
        [InlineData("", "forest", 1, 1, 1900)]
        [InlineData("wood", "", 1, 1, 1900)]
        [InlineData("wood", "forest", 1, 1, 2100)]
        public void EditCustomer(string first, string last, int day, int month, int year)
        {
            var c = new Customer
            {
                FirstName = first,
                LastName = last,
                DateOfBirth = new DateTime(year, month, day)
            };
            _serviceMock.Setup(s => s.AddEditCustomer(c, true)).Returns(new Response<Customer>());

            var controller = new CustomerController(_serviceMock.Object);
            var res = controller.EditCustomer(c);

            if (!c.IsValid(true))
                Assert.Equal(typeof(BadRequestObjectResult), res.Result.GetType());
            else
                Assert.Equal(typeof(OkObjectResult), res.Result.GetType());
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        public void DeleteCustomer_Error(int id)
        {
            _serviceMock.Setup(s => s.DeleteCustomer_Id(id)).Returns(new Response<Customer> { Exists = false });

            var controller = new CustomerController(_serviceMock.Object);
            var res = controller.DeleteCustomer(id);

            Assert.Equal(typeof(BadRequestObjectResult), res.Result.GetType());
        }

        [Fact]
        public void DeleteCustomer()
        {
            _serviceMock.Setup(s => s.DeleteCustomer_Id(1)).Returns(new Response<Customer> { Exists = true });

            var controller = new CustomerController(_serviceMock.Object);
            var res = controller.DeleteCustomer(1);

            Assert.Equal(typeof(OkObjectResult), res.Result.GetType());
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void FindCustomer(bool exists)
        {
            _serviceMock.Setup(s => s.FindCustomer_FirstAndLastName("")).Returns(new Response<IEnumerable<Customer>> { Exists = exists });

            var controller = new CustomerController(_serviceMock.Object);
            var res = controller.FindCustomer("");

            if (!exists)
                Assert.Equal(typeof(BadRequestObjectResult), res.Result.GetType());
            else
                Assert.Equal(typeof(OkObjectResult), res.Result.GetType());
        }


    }
}
