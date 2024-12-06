using brH60Store.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace brH60Store.DAL {
    public class ProductRepository : IProductRepository {
        private readonly HttpClient _httpClient;
        private string endpoint = "http://localhost:64528/api/products";

        public ProductRepository(HttpClient httpClient) {
            _httpClient = httpClient;

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<bool> DeleteProduct(int id) {
            HttpResponseMessage res = await _httpClient.DeleteAsync(endpoint + $"/{id}");
            return res.IsSuccessStatusCode;
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

        public async Task<bool> InsertProduct(Product product) {
            var jsonProdCat = JsonConvert.SerializeObject(product);
            var httpContent = new StringContent(jsonProdCat, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, httpContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateProduct(Product product) {
            var jsonProd = JsonConvert.SerializeObject(product);
            var httpContent = new StringContent(jsonProd, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(endpoint + $"/{product.ProductId}", httpContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<List<ProductCategory>> GetCategories() {
            List<ProductCategory> categoriesList = null;
            SelectList categoriesSelectList = null;
            HttpResponseMessage res = await _httpClient.GetAsync(endpoint + "/GetCategories");

            if (res.IsSuccessStatusCode) {
                string data = await res.Content.ReadAsStringAsync();
                categoriesList = JsonConvert.DeserializeObject<List<ProductCategory>>(data);
                return categoriesList;
            }

            return categoriesList;
        }

    }
}
