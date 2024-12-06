using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using brH60Services.DAL;
using brH60Services.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace brH60Services.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase {
        private readonly IProductRepository _storeRepository;

        public ProductsController(IProductRepository storeRepository) { 
            _storeRepository = storeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Products() {
            var products = await _storeRepository.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Product(int id) {
            var product = await _storeRepository.GetProductById(id);

            if (product == null) {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("ByCategory")]
        public async Task<ActionResult<IEnumerable<Product>>> ProductsByCategory() {
            var products = await _storeRepository.GetProductsByCategory();
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product product) {
            await _storeRepository.InsertProduct(product);
            await _storeRepository.Save();
            return CreatedAtAction("Product", new { id = product.ProductId }, product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product) {
            if (id != product.ProductId) {
                return BadRequest();
            }
            _storeRepository.UpdateProduct(product);

            try {
                await _storeRepository.Save();
            } catch (DbUpdateConcurrencyException) {
                if (!_storeRepository.ProductExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }
            return CreatedAtAction("Product", new { id = product.ProductId }, product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id) {
            var product = await _storeRepository.GetProductById(id);
            if (product == null) {
                return NotFound();
            }

            _storeRepository.DeleteProduct(product);
            await _storeRepository.Save();

            return Ok();
        }

        [HttpGet("GetCategories")]
        public async Task<ActionResult<List<ProductCategory>>> GetCategories() {
            return _storeRepository.GetCategories();
        }

        //[HttpPost("GetCategoriesWithProduct")]
        //public async Task<ActionResult<SelectList>> GetCategoriesWithProduct(Product product) {
        //    return _storeRepository.GetCategoriesWithProduct(product);
        //}
    }
}
