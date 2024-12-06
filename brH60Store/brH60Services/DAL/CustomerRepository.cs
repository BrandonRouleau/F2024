using Microsoft.EntityFrameworkCore;
using brH60Services.Models;

namespace brH60Services.DAL {
    public class CustomerRepository : ICustomerRepository {
        private readonly H60assignment2DbBrContext _context;

        public CustomerRepository(H60assignment2DbBrContext context) { 
            _context = context;
        }

        public bool DeleteCustomer(Customer customer) {
            _context.Customers.Remove(customer);
            return true;
        }

        public async Task<Customer> GetCustomerById(int? id) {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            return customer;
        }

        public async Task<IEnumerable<Customer>> GetCustomers() {
            var customers = await _context.Customers.OrderBy(p => p.FirstName).ThenBy(p => p.LastName).ToListAsync();
            return customers;
        }

        public async Task InsertCustomer(Customer customer) {
            await _context.AddAsync(customer);
        }

        public async Task Save() {
            await _context.SaveChangesAsync();
        }

        public bool UpdateCustomer(Customer customer) {
            _context.Update(customer);
            return true;
        }

        public bool CustomerExists(int id) {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
