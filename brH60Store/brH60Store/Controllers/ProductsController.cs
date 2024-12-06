using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using brH60Store.Models;
using brH60Store.DAL;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Authorization;

namespace brH60Store.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: Products
        [HttpGet]
        [Authorize(Roles = "Manager,Clerk")]
        public async Task<IActionResult> Index() {
            List<Product> products = await _productRepository.GetProducts();
            return View(products);
        }

        // GET: Products/Details/5
        [Authorize(Roles = "Manager,Clerk")]
        public async Task<IActionResult> Details(int id)
        {
            Product product = await _productRepository.GetProductById(id);

            if (product == null) {
                return RedirectToAction("Index");
            }

            return View(product);
        }

        [Authorize(Roles = "Manager,Clerk")]
        public async Task<IActionResult> ProductsByCategory() {
            List<Product> products = await _productRepository.GetProductsByCategory();
            return View(products);
        }

        // GET: Products/Create
        [Authorize(Roles = "Manager,Clerk")]
        public async Task<IActionResult> Create()
        {
            ViewData["ProdCatId"] = new SelectList(await _productRepository.GetCategories(), "CategoryId", "CategoryId");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager,Clerk")]
        public async Task<IActionResult> Create([Bind("ProductId,ProdCatId,Description,Manufacturer,Stock,BuyPrice,SellPrice")] Product product)
        {
            if(ModelState.ContainsKey("ProdCat")) {
                ModelState.Remove("ProdCat");
            }
            if (ModelState.IsValid)
            {
                bool prodCat = await _productRepository.InsertProduct(product);

                if (prodCat) {
                    return RedirectToAction(nameof(Index));
                }
                ViewData["ProdCatId"] = new SelectList(await _productRepository.GetCategories(), "CategoryId", "CategoryId", product.ProdCatId);
                return View(product);
            }
            ViewData["ProdCatId"] = new SelectList(await _productRepository.GetCategories(), "CategoryId", "CategoryId", product.ProdCatId);
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Manager,Clerk")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null) return NotFound();

            //Get product from id
            Product product = await _productRepository.GetProductById(id);
            if (product == null) return NotFound();

            //Get categories select list based on the product
            ViewData["ProdCatId"] = ViewData["ProdCatId"] = new SelectList(await _productRepository.GetCategories(), "CategoryId", "CategoryId", product.ProdCatId); ;
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager,Clerk")]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProdCatId,Description,Manufacturer,Stock,BuyPrice,SellPrice")] Product product)
        {
            bool updated = false;
            if (id != product.ProductId)
            {
                return NotFound();
            }
            if (ModelState.ContainsKey("ProdCat")) {
                ModelState.Remove("ProdCat");
            }
            if (ModelState.IsValid)
            {
                var jsonProd = await _productRepository.UpdateProduct(product);
                if(jsonProd) {
                    return RedirectToAction(nameof(Index));
                }
            }

            //Get category select list based on product
            ViewData["ProdCatId"] = new SelectList(await _productRepository.GetCategories(), "CategoryId", "CategoryId", product.ProdCatId);
            return View(product);
        }

        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> UpdateStock(int id) {
            if (id == null) return NotFound();

            var product = await _productRepository.GetProductById(id);
            if (product == null) return NotFound();

            ViewData["ProdCatId"] = new SelectList(await _productRepository.GetCategories(), "CategoryId", "CategoryId", product.ProdCatId);
            return View(product);
        }

        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> UpdateStock(int id, [Bind("ProductId,ProdCatId,Description,Manufacturer,Stock,BuyPrice,SellPrice")] Product product) {
            bool updated = false;
            if (id != product.ProductId) {
                return NotFound();
            }
            if (ModelState.ContainsKey("ProdCat")) {
                ModelState.Remove("ProdCat");
            }
            if (ModelState.IsValid) {
                var jsonProd = await _productRepository.UpdateProduct(product);
                if (jsonProd) {
                    return RedirectToAction(nameof(Index));
                }
            }

            //Get category select list based on product
            ViewData["ProdCatId"] = new SelectList(await _productRepository.GetCategories(), "CategoryId", "CategoryId", product.ProdCatId);
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Manager,Clerk")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null) return NotFound();

            Product product = await _productRepository.GetProductById(id);

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager,Clerk")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id == null) return NotFound();

            bool res = await _productRepository.DeleteProduct(id);

            if (res) {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
