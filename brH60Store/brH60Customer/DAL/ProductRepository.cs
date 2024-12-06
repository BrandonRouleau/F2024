using brH60Customer.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace brH60Customer.DAL {
    public class ProductRepository : IProductRepository {
        private readonly HttpClient _httpClient;
        private string endpoint = "http://localhost:64528/api/products";

        public ProductRepository(HttpClient httpClient) {
            _httpClient = httpClient;

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<Product> GetProductById(int id) {
            Product product = null;
            HttpResponseMessage res = await _httpClient.GetAsync(endpoint + $"/{id}");

            if (res.IsSuccessStatusCode) {
                string data = await res.Content.ReadAsStringAsync();
                product = JsonConvert.DeserializeObject<Product>(data);
            }

            return product;
        }

        public async Task<List<Product>> GetProductsByCategory() {
            List<Product> products = null;
            HttpResponseMessage res = await _httpClient.GetAsync(endpoint + "/ProductsByCategory");

            if (res.IsSuccessStatusCode) {
                string data = await res.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<Product>>(data);
            }

            if (products == null) {
                products = new List<Product>();
            }

            return products;
        }

        public async Task<List<Product>> GetProducts() {
            List<Product> products = null;
            HttpResponseMessage res = await _httpClient.GetAsync(endpoint);

            if (res.IsSuccessStatusCode) {
                string data = await res.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<Product>>(data);
            }

            if (products == null) {
                products = new List<Product>();
            }
            return products;
        }

    }
}
