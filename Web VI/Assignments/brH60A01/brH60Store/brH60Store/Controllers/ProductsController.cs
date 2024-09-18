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
using Microsoft.Build.Framework;

namespace brH60Store.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _storeRepository;

        public ProductsController(IProductRepository storeRepo)
        {
            _storeRepository = storeRepo;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(_storeRepository.GetProducts());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var product = _storeRepository.GetProductById(id);
            if (product == null) return NotFound();

            return View(product);
        }

        public IActionResult ProductsByCategory() {
            return View(_storeRepository.GetProductsByCategory());
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["ProdCatId"] = _storeRepository.GetCategories();
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProdCatId,Description,Manufacturer,Stock,BuyPrice,SellPrice,Image")] Product product)
        {
            if(ModelState.ContainsKey("ProdCat")) {
                ModelState.Remove("ProdCat");
            }
            if (ModelState.IsValid)
            {
                if(product.Image == null) {
                    product.Image = "./img/Default-img.jfif";
                }
                _storeRepository.InsertProduct(product);
                await _storeRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProdCatId"] = _storeRepository.GetCategoriesWithProduct(product);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = _storeRepository.GetProductById(id);
            if (product == null) return NotFound();

            ViewData["ProdCatId"] = _storeRepository.GetCategoriesWithProduct(product);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProdCatId,Description,Manufacturer,Stock,BuyPrice,SellPrice,Image")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }
            if (ModelState.ContainsKey("ProdCat")) {
                ModelState.Remove("ProdCat");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (product.Image == null) {
                        product.Image = "./img/Default-img.jfif";
                    }
                    _storeRepository.UpdateProduct(product);
                    await _storeRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_storeRepository.ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProdCatId"] = _storeRepository.GetCategoriesWithProduct(product);
            return View(product);
        }

        public async Task<IActionResult> UpdateStock(int? id) {
            if (id == null) return NotFound();

            var product = _storeRepository.GetProductById(id);
            if (product == null) return NotFound();

            ViewData["ProdCatId"] = _storeRepository.GetCategoriesWithProduct(product);
            return View(product);
        }

        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStock(int id, [Bind("ProductId,ProdCatId,Description,Manufacturer,Stock,BuyPrice,SellPrice,Image")] Product product) {
            if (id != product.ProductId) {
                return NotFound();
            }
            if (ModelState.ContainsKey("ProdCat")) {
                ModelState.Remove("ProdCat");
            }

            if (ModelState.IsValid) {
                try {
                    Product productUpdate = _storeRepository.GetProductById(product.ProductId);
                    if (productUpdate.updateStock(product.Stock)) {
                        _storeRepository.UpdateProduct(productUpdate);
                        await _storeRepository.Save();
                    }
                    else {
                        return View(product);
                    }
                } catch (DbUpdateConcurrencyException) {
                    if (!_storeRepository.ProductExists(product.ProductId)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProdCatId"] = _storeRepository.GetCategoriesWithProduct(product);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var product = _storeRepository.GetProductById(id);
            if (product == null) return NotFound();

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = _storeRepository.GetProductById(id);
            if (product != null)
            {
                _storeRepository.DeleteProduct(product);
            }
            await _storeRepository.Save();
            return RedirectToAction(nameof(Index));
        }

    }
}
