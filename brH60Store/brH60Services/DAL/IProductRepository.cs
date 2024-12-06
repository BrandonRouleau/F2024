using Microsoft.AspNetCore.Mvc.Rendering;
using brH60Services.Models;

namespace brH60Services.DAL {
    public interface IProductRepository {
        Task<IEnumerable<Product>> GetProducts();

        Task<Product> GetProductById(int? id);

        Task<IEnumerable<Product>> GetProductsByCategory();

        bool ProductExists(int id);

        Task InsertProduct(Product product);

        bool DeleteProduct(Product product);

        bool UpdateProduct(Product product);

        List<ProductCategory> GetCategories();

        //SelectList GetCategoriesWithProduct(Product product);

        Task Save();
    }
}
