using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using brH60Services.DAL;
using brH60Services.Models;
using Microsoft.EntityFrameworkCore;

namespace brH60Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase {
        private readonly ICustomerRepository _storeRepository;

        public CustomersController(ICustomerRepository storeRepository) {
            _storeRepository = storeRepository;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id) {
            var customer = await _storeRepository.GetCustomerById(id);
            if (customer == null) {
                return NotFound();
            }

            _storeRepository.DeleteCustomer(customer);
            await _storeRepository.Save();

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int? id) {
            var customer = await _storeRepository.GetCustomerById(id);

            if (customer == null) {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers() {
            var customers = await _storeRepository.GetCustomers();
            return Ok(customers);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> AddCustomer(Customer customer) {
            _storeRepository.InsertCustomer(customer);
            await _storeRepository.Save();

            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> UpdateCustomer(int id, Customer customer) {
            if (id != customer.CustomerId) {
                return BadRequest();
            }
            _storeRepository.UpdateCustomer(customer);

            try {
                await _storeRepository.Save();
            } catch (DbUpdateConcurrencyException) {
                if (!_storeRepository.CustomerExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }
            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }


    }
}
