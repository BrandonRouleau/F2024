using brH60Services.Models;
using Microsoft.EntityFrameworkCore;

namespace brH60Services.DAL {
    public class ProductCategoryRepository : IProductCategoryRepository {
        private readonly H60assignment2DbBrContext _context;

        public ProductCategoryRepository(H60assignment2DbBrContext context) {
            _context = context;
        }

        public void DeleteProductCategory(ProductCategory productCategory) {
            if (productCategory != null) {
                _context.ProductCategories.Remove(productCategory);
            }
        }

        public async Task<IEnumerable<ProductCategory>> GetProductCategories() {
            return await _context.ProductCategories.OrderBy(p => p.ProdCat).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetCategoryProducts(int? id) {
            return await _context.Products.Where(p => p.ProdCatId == id).ToListAsync();
        }

        public async Task<ProductCategory> GetProductCategoryById(int? id) {
            var productCategory = await _context.ProductCategories
                .FirstOrDefaultAsync(p => p.CategoryId == id);
            return productCategory;
        }

        public void InsertProductCategory(ProductCategory productCategory) {
            _context.Add(productCategory); ;
        }

        public async Task Save() {
            await _context.SaveChangesAsync();
        }

        public void UpdateProductCategory(ProductCategory productCategory) {
            _context.Update(productCategory);
        }

        public bool ProductCategoryExists(int id) {
            return _context.ProductCategories.Any(e => e.CategoryId == id);
        }
    }
}
