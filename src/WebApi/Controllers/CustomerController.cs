using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [Route("customers")]
    public class CustomerController : Controller
    {
        private ICustomerRepository<Customer> CustomerRepository { get; }
        public CustomerController(ICustomerRepository<Customer> customerRepository)
        {
            CustomerRepository = customerRepository;
        }

        [HttpGet("{id:long}")]   
        public async Task<Customer> GetCustomerAsync([FromRoute] long id)
        {
            return await CustomerRepository.GetAsync(id);
        }

        [HttpPost("")]   
        public async Task<long> CreateCustomerAsync([FromBody] Customer customer)
        {
            return await CustomerRepository.CreateAsync(customer);
        }
    }
}