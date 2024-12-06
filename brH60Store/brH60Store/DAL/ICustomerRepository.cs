using brH60Store.Models;

namespace brH60Store.DAL {
    public interface ICustomerRepository {
        public Task<List<Customer>> GetCustomers();

        public Task<Customer> GetCustomer(int id);

        public Task<bool> AddCustomer(Customer customer);

        public Task<bool> EditCustomer(int id, Customer customer);

        public Task<bool> DeleteCustomer(int id);
    }
}
