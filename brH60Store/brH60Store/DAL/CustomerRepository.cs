using brH60Store.Models;
using Newtonsoft.Json;
using System.Text;

namespace brH60Store.DAL {
    public class CustomerRepository : ICustomerRepository {
        private readonly HttpClient _httpClient;
        private string endpoint = "http://localhost:64528/api/customers";

        public CustomerRepository(HttpClient httpClient) {
            _httpClient = httpClient;

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<List<Customer>> GetCustomers() {
            List<Customer> customers = null;
            HttpResponseMessage res = await _httpClient.GetAsync(endpoint);

            if (res.IsSuccessStatusCode) {
                string data = await res.Content.ReadAsStringAsync();
                customers = JsonConvert.DeserializeObject<List<Customer>>(data);
            }

            if (customers == null) {
                customers = new List<Customer>();
            }
            return customers;
        }

        public async Task<Customer> GetCustomer(int id) {
            Customer customer = null;
            HttpResponseMessage res = await _httpClient.GetAsync(endpoint + $"/{id}");

            if (res.IsSuccessStatusCode) {
                string data = await res.Content.ReadAsStringAsync();
                customer = JsonConvert.DeserializeObject<Customer>(data);
            }

            if (customer == null) {
                return new Customer();
            }

            return customer;
        }

        public async Task<bool> AddCustomer(Customer customer) {
            var jsonCustomer = JsonConvert.SerializeObject(customer);
            var httpContent = new StringContent(jsonCustomer, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, httpContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EditCustomer(int id, Customer customer) {
            var jsonProd = JsonConvert.SerializeObject(customer);
            var httpContent = new StringContent(jsonProd, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(endpoint + $"/{id}", httpContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCustomer(int id) {
            HttpResponseMessage res = await _httpClient.DeleteAsync(endpoint + $"/{id}");
            return res.IsSuccessStatusCode;
        }

    }
}
