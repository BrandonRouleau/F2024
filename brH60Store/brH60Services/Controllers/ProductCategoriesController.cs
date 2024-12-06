using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using brH60Services.DAL;
using brH60Services.Models;
using Microsoft.EntityFrameworkCore;

namespace brH60Services.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoriesController : ControllerBase {
        private readonly IProductCategoryRepository _storeRepository;

        public ProductCategoriesController(IProductCategoryRepository storeRepository) { 
            _storeRepository = storeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetCategories() {
            var categories = await _storeRepository.GetProductCategories();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCategory(int id) { 
            var category = await _storeRepository.GetProductCategoryById(id);

            if (category == null) {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpGet("CategoryProducts/{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> CategoryProducts(int id) {
            var products = await _storeRepository.GetCategoryProducts(id);
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<ProductCategory>> AddProductCategory(ProductCategory productCategory) {
            _storeRepository.InsertProductCategory(productCategory);
            await _storeRepository.Save();

            return CreatedAtAction("GetCategory", new { id = productCategory.CategoryId }, productCategory);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProductCategory(int id, ProductCategory productCategory) {
            if (id != productCategory.CategoryId) {
                return BadRequest();
            }
            _storeRepository.UpdateProductCategory(productCategory);

            try {
                await _storeRepository.Save();
            } catch (DbUpdateConcurrencyException) {
                if (!_storeRepository.ProductCategoryExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }
            return CreatedAtAction("GetCategory", new { id = productCategory.CategoryId }, productCategory);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductCategory(int id) {
            var productCategory = await _storeRepository.GetProductCategoryById(id);
            if (productCategory == null) {
                return NotFound();
            }

            _storeRepository.DeleteProductCategory(productCategory);
            await _storeRepository.Save();

            return Ok();
        }
    }
}
