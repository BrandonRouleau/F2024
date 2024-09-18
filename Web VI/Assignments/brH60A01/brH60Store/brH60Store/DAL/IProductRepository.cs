using brH60Store.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace brH60Store.DAL {
    public interface IProductRepository {
        IEnumerable<Product> GetProducts();

        Product GetProductById(int? id);

        IEnumerable<Product> GetProductsByCategory();

        bool ProductExists(int id);

        void InsertProduct(Product product);

        void DeleteProduct(Product product);

        void UpdateProduct(Product product);

        SelectList GetCategories();

        SelectList GetCategoriesWithProduct(Product product);

        Task Save();
    }
}
