using brH60Store.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace brH60Store.DAL {
    public class ProductRepository : IProductRepository {
        private readonly H60assignmentDbBrContext _context;

        public ProductRepository(H60assignmentDbBrContext context) { 
            _context = context;
        }

        public void DeleteProduct(Product product) {
            _context.Products.Remove(product);
        }

        public Product GetProductById(int? id) {
            var product = _context.Products
                .Include(p => p.ProdCat)
                .FirstOrDefault(m => m.ProductId == id);
            return product;
        }

        public IEnumerable<Product> GetProductsByCategory() { 
            return _context.Products.Include(p => p.ProdCat).OrderBy(p => p.ProdCat).ThenBy(p => p.Description).ToList();
        }

        public IEnumerable<Product> GetProducts() {
            var products = _context.Products.Include(p => p.ProdCat).OrderBy(p => p.Description).ToList();
            return products;
        }

        public void InsertProduct(Product product) {
            _context.Add(product);
        }

        public async Task Save() {
            await _context.SaveChangesAsync();
        }

        public void UpdateProduct(Product product) {
            _context.Update(product);
        }

        public SelectList GetCategories() {
            return new SelectList(_context.ProductCategories, "CategoryId", "CategoryId");
        }

        public SelectList GetCategoriesWithProduct(Product product) {
            return new SelectList(_context.ProductCategories, "CategoryId", "CategoryId", product.ProdCatId);
        }

        public bool ProductExists(int id) {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
