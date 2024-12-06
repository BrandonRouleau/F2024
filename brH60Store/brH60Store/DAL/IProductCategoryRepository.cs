using brH60Store.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace brH60Store.DAL {
    public interface IProductCategoryRepository {
        Task<List<ProductCategory>> GetProductCategories();

        Task<ProductCategory> GetProductCategory(int id);

        Task<List<Product>> GetCategoryProducts(int id);

        Task<bool> AddProductCategry(ProductCategory productCategory);

        Task<bool> EditProductCategory(int id, ProductCategory productCategory);

        Task<bool> DeleteProductCategory(int id);
    }
}
