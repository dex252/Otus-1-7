using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [Route("customers")]
    public class CustomerController : Controller
    {
        private ICustomerRepository<Customer> CustomerRepository { get; }
        private int MinIdValue { get; }
        private int MaxIdValue { get; }
        public CustomerController(ICustomerRepository<Customer> customerRepository, IConfiguration configuration)
        {
            CustomerRepository = customerRepository;
            MinIdValue = int.Parse(configuration["IdRangeCustomers:Min"]);
            MaxIdValue = int.Parse(configuration["IdRangeCustomers:Max"]);
        }

        [HttpGet("{id:long}")]   
        public async Task<Customer> GetCustomerAsync([FromRoute] long id)
        {
            return await CustomerRepository.GetAsync(id);
        }

        [HttpPost("")]   
        public async Task<long> CreateCustomerAsync([FromBody] Customer customer)
        {
            var rnd = new Random();
            customer.Id = rnd.Next(MinIdValue, MaxIdValue);

            await CustomerRepository.CreateAsync(customer);
            return customer.Id;
        }
    }
}