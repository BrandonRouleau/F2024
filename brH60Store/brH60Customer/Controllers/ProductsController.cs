using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using brH60Customer.Models;
using brH60Customer.DAL;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace brH60Customer.Controllers {
    public class ProductsController : Controller {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository) {
            _productRepository = productRepository;
        }

        // GET: Products
        [HttpGet]
        public async Task<IActionResult> Index() {
            List<Product> products = await _productRepository.GetProducts();
            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int id) {
            Product product = await _productRepository.GetProductById(id);

            if (product == null) {
                return RedirectToAction("Index");
            }

            return View(product);
        }

        public async Task<IActionResult> ProductsByCategory() {
            List<Product> products = await _productRepository.GetProductsByCategory();
            return View(products);
        }

       

    }
}
