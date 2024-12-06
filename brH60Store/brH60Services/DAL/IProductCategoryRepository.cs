using brH60Services.Models;

namespace brH60Services.DAL {
    public interface IProductCategoryRepository {
        Task<IEnumerable<ProductCategory>> GetProductCategories();

        Task<ProductCategory> GetProductCategoryById(int? id);

        Task<IEnumerable<Product>> GetCategoryProducts(int? id);

        bool ProductCategoryExists(int id);

        void InsertProductCategory(ProductCategory productCategory);

        void DeleteProductCategory(ProductCategory productCategory);

        void UpdateProductCategory(ProductCategory productCategory);

        Task Save();
    }
}
