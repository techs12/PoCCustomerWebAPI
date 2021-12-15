using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PocCustomer.Model;
using PoCCustomer.Service;

namespace PoC.CustomerWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly string customerInvalidMessage = "Parameters provided are not valid.";
        public CustomerController(ICustomerService service)
        {
            _customerService = service;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Customer> AddCustomer([FromBody] Customer customer)
        {
            if (!customer.IsValid())
                return BadRequest(customerInvalidMessage);

            var res = _customerService.AddEditCustomer(customer);
            if (res.Exists) return BadRequest("Customer already exists.");

            return Created(nameof(AddCustomer), res.Customer);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Customer> EditCustomer([FromBody] Customer customer)
        {
            if (!customer.IsValid(true))
                return BadRequest(customerInvalidMessage);

            var res = _customerService.AddEditCustomer(customer, true);
            if (!res.Exists) return BadRequest("Customer does not exist.");

            return Ok(res.Customer);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Customer> DeleteCustomer(int id)
        {
            if (id <= 0)
                return BadRequest(customerInvalidMessage);

            var res = _customerService.DeleteCustomer_Id(id);
            if (!res.Exists) return BadRequest("Customer does not exist.");

            return Ok(res.Customer);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<Customer>> FindCustomer(string searchCriteria)
        {
            var res = _customerService.FindCustomer_FirstAndLastName(searchCriteria);
            if (!res.Exists)
                return BadRequest("The search criteria cannot be empty.");

            return Ok(res.Customer);
        }

    }
}

