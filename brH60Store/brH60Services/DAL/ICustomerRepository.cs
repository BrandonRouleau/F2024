using brH60Services.Models;

namespace brH60Services.DAL {
    public interface ICustomerRepository {
        bool DeleteCustomer(Customer customer);

        Task<Customer> GetCustomerById(int? id);

        Task<IEnumerable<Customer>> GetCustomers();

        Task InsertCustomer(Customer customer);

        Task Save();

        bool UpdateCustomer(Customer customer);

        bool CustomerExists(int id);
    }
}
