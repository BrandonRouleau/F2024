using brH60Services.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace brH60Services.DAL {
    public class ProductRepository : IProductRepository {
        private readonly H60assignment2DbBrContext _context;

        public ProductRepository(H60assignment2DbBrContext context) {
            _context = context;
        }

        public bool DeleteProduct(Product product) {
            _context.Products.Remove(product);
            return true;
        }

        public async Task<Product> GetProductById(int? id) {
            var product = await _context.Products
                .Include(p => p.ProdCat)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            return product;
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory() {
            return await _context.Products.Include(p => p.ProdCat).OrderBy(p => p.ProdCat).ThenBy(p => p.Description).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts() {
            var products = await _context.Products.Include(p => p.ProdCat).OrderBy(p => p.Description).ToListAsync();
            return products;
        }

        public async Task InsertProduct(Product product) {
            await _context.AddAsync(product);
        }

        public async Task Save() {
            await _context.SaveChangesAsync();
        }

        public bool UpdateProduct(Product product) {
            _context.Update(product);
            return true;
        }

        public List<ProductCategory> GetCategories() {
            return new List<ProductCategory>(_context.ProductCategories);
        }

        //public SelectList GetCategoriesWithProduct(Product product) {
        //    return new SelectList(_context.ProductCategories, "CategoryId", "CategoryId", product.ProdCatId);
        //}

        public bool ProductExists(int id) {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
