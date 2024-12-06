using brH60Store.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace brH60Store.DAL {
    public interface IProductRepository {
        Task<bool> DeleteProduct(int id);

        Task<Product> GetProductById(int id);

        Task<List<Product>> GetProductsByCategory();

        Task<List<Product>> GetProducts();

        Task<bool> InsertProduct(Product product);

        Task<bool> UpdateProduct(Product product);

        Task<List<ProductCategory>> GetCategories();
    }
}
