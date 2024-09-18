using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using brH60Store.Models;
using brH60Store.DAL;

namespace brH60Store.Controllers
{
    public class ProductCategoriesController : Controller
    {
        private readonly IProductCategoryRepository _storeRepository;

        public ProductCategoriesController(IProductCategoryRepository storeRepo) {
            _storeRepository = storeRepo;
        }

        // GET: ProductCategories
        public async Task<IActionResult> Index()
        {
            return View(_storeRepository.GetProductCategories());
        }

        // GET: ProductCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var productCategory = _storeRepository.GetProductCategoryById(id);
            if (productCategory == null) return NotFound();

            return View(productCategory);
        }

        public async Task<IActionResult> CategoryProducts(int? id) {
            if (id == null) return NotFound();

            var products = _storeRepository.GetCategoryProducts(id);
            return View(products);
        }

        // GET: ProductCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,ProdCat,Image")] ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                if (productCategory.Image == null) {
                    productCategory.Image = "./img/Default-img.jfif";
                }
                _storeRepository.InsertProductCategory(productCategory);
                await _storeRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(productCategory);
        }

        // GET: ProductCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategory = _storeRepository.GetProductCategoryById(id);
            if (productCategory == null)
            {
                return NotFound();
            }
            return View(productCategory);
        }

        // POST: ProductCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,ProdCat, Image")] ProductCategory productCategory)
        {
            if (id != productCategory.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(productCategory.Image == null) {
                        productCategory.Image = "./img/Default-img.jfif";
                    }
                    _storeRepository.UpdateProductCategory(productCategory);
                    await _storeRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_storeRepository.ProductCategoryExists(productCategory.CategoryId))
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
            return View(productCategory);
        }

        // GET: ProductCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var productCategory = _storeRepository.GetProductCategoryById(id);
            if (productCategory == null) return NotFound();

            return View(productCategory);
        }

        // POST: ProductCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productCategory = _storeRepository.GetProductCategoryById(id);
            _storeRepository.DeleteProductCategory(productCategory);

            await _storeRepository.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
