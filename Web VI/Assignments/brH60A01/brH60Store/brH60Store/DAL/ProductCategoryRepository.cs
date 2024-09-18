using brH60Store.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace brH60Store.DAL {
    public class ProductCategoryRepository : IProductCategoryRepository {
        private readonly H60assignmentDbBrContext _context;

        public ProductCategoryRepository(H60assignmentDbBrContext context) {
            _context = context;
        }

        public void DeleteProductCategory(ProductCategory productCategory) {
            if (productCategory != null) {
                _context.ProductCategories.Remove(productCategory);
            }
        }

        public IEnumerable<ProductCategory> GetProductCategories() {
            return _context.ProductCategories.OrderBy(p => p.ProdCat).ToList();
        }

        public IEnumerable<Product> GetCategoryProducts(int? id) {
            return _context.Products.Where(p => p.ProdCatId == id).ToList();
        }

        public ProductCategory GetProductCategoryById(int? id) {
            var productCategory = _context.ProductCategories
                .FirstOrDefault(p => p.CategoryId == id);
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
