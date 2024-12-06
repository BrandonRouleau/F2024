using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using brH60Customer.Models;
using brH60Customer.DAL;
using Newtonsoft.Json;
using System.Text;

namespace brH60Customer.Controllers {
    public class ProductCategoriesController : Controller {
        private readonly IProductCategoryRepository _prodCatRepository;

        public ProductCategoriesController(IProductCategoryRepository prodCatRepository) {
            _prodCatRepository = prodCatRepository;
        }

        // GET: ProductCategories
        [HttpGet]
        public async Task<IActionResult> Index() {
            List<ProductCategory> categories = await _prodCatRepository.GetProductCategories();
            return View(categories);
        }

        // GET: ProductCategories/Details/5
        public async Task<IActionResult> Details(int id) {
            ProductCategory category = await _prodCatRepository.GetProductCategory(id);

            if (category == null) {
                return RedirectToAction("Index");
            }

            return View(category);
        }

        [HttpGet("CategroyProducts/{id}")]
        public async Task<IActionResult> CategoryProducts(int id) {
            List<Product> products = await _prodCatRepository.GetCategoryProducts(id);

            return View(products);
        }
    }
}
