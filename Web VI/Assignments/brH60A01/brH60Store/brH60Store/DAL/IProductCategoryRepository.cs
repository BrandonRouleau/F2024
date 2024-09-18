using brH60Store.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace brH60Store.DAL {
    public interface IProductCategoryRepository {
        IEnumerable<ProductCategory> GetProductCategories();

        ProductCategory GetProductCategoryById(int? id);

        IEnumerable<Product> GetCategoryProducts(int? id);

        bool ProductCategoryExists(int id);

        void InsertProductCategory(ProductCategory productCategory);

        void DeleteProductCategory(ProductCategory productCategory);

        void UpdateProductCategory(ProductCategory productCategory);

        Task Save();
    }
}
