using brH60Customer.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace brH60Customer.DAL {
    public interface IProductCategoryRepository {
        Task<List<ProductCategory>> GetProductCategories();

        Task<ProductCategory> GetProductCategory(int id);

        Task<List<Product>> GetCategoryProducts(int id);
    }
}
