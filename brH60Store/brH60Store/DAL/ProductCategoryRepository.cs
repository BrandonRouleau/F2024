using brH60Store.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace brH60Store.DAL {
    public class ProductCategoryRepository : IProductCategoryRepository {
        private readonly HttpClient _httpClient;
        private string endpoint = "http://localhost:64528/api/ProductCategories";

        public ProductCategoryRepository(HttpClient httpClient) {
            _httpClient = httpClient;

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<List<ProductCategory>> GetProductCategories() {
            List<ProductCategory> prodCats = null;
            HttpResponseMessage res = await _httpClient.GetAsync(endpoint);

            if (res.IsSuccessStatusCode) {
                string data = await res.Content.ReadAsStringAsync();
                prodCats = JsonConvert.DeserializeObject<List<ProductCategory>>(data);
            }

            if (prodCats == null) {
                prodCats = new List<ProductCategory>();
            }
            return prodCats;
        }

        public async Task<ProductCategory> GetProductCategory(int id) {
            ProductCategory prodCat = null;
            HttpResponseMessage res = await _httpClient.GetAsync(endpoint + $"/{id}");

            if (res.IsSuccessStatusCode) {
                string data = await res.Content.ReadAsStringAsync();
                prodCat = JsonConvert.DeserializeObject<ProductCategory>(data);
            }

            if (prodCat == null) {
                return prodCat;
            }

            return prodCat;
        }

        public async Task<bool> AddProductCategry(ProductCategory productCategory) {
            var jsonCustomer = JsonConvert.SerializeObject(productCategory);
            var httpContent = new StringContent(jsonCustomer, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, httpContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EditProductCategory(int id, ProductCategory productCategory) {
            var jsonProd = JsonConvert.SerializeObject(productCategory);
            var httpContent = new StringContent(jsonProd, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(endpoint + $"/{id}", httpContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProductCategory(int id) {
            HttpResponseMessage res = await _httpClient.DeleteAsync(endpoint + $"/{id}");
            return res.IsSuccessStatusCode;
        }

        public async Task<List<Product>> GetCategoryProducts(int id) {
            List<Product> products = null;
            HttpResponseMessage res = await _httpClient.GetAsync(endpoint + $"/CategoryProducts/{id}");

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
