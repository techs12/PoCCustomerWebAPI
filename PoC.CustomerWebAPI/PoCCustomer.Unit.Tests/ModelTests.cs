using PocCustomer.Model;
using System;
using Xunit;

namespace PoCCustomer.Unit.Tests
{
    public class ModelTests
    {
        [Fact]
        public void CheckProperties()
        {
            var properties = typeof(Customer).GetProperties();

            Assert.Equal(4, properties.Length);
            Assert.Equal("Id", properties[0].Name);
            Assert.Equal("FirstName", properties[1].Name);
            Assert.Equal("LastName", properties[2].Name);
            Assert.Equal("DateOfBirth", properties[3].Name);
        }

        [Fact]
        public void IsValid()
        {
            var customer = new Customer
            {
                FirstName = "wood",
                LastName = "forest",
                DateOfBirth = new DateTime(1900, 1, 1)
            };

            Assert.True(customer.IsValid());
        }

        [Fact]
        public void IsValid_Id()
        {
            var customer = new Customer
            {
                Id = 1,
                FirstName = "wood",
                LastName = "forest",
                DateOfBirth = new DateTime(1900, 1, 1)
            };

            Assert.True(customer.IsValid(true));
        }

        [Fact]
        public void IsNotValid_Id()
        {
            var customer = new Customer
            {
                Id = 0,
                FirstName = "wood",
                LastName = "forest",
                DateOfBirth = new DateTime(1900, 1, 1)
            };

            Assert.False(customer.IsValid(true));
        }

        [Theory]
        [InlineData("", "forest", 1, 1, 1900)]
        [InlineData("wood", "", 1, 1, 1900)]
        [InlineData("wood", "forest", 1, 1, 2100)]
        public void IsNotValid(string first, string last, int day, int month, int year)
        {
            var customer = new Customer
            {
                FirstName = first,
                LastName = last,
                DateOfBirth = new DateTime(year, month, day)
            };

            Assert.False(customer.IsValid());
        }
    }
}
