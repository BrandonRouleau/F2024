using brH60Customer.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace brH60Customer.DAL {
    public interface IProductRepository {
        Task<Product> GetProductById(int id);

        Task<List<Product>> GetProductsByCategory();

        Task<List<Product>> GetProducts();
    }
}
