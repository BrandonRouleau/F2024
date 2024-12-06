using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using brH60Store.Models;
using brH60Store.DAL;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace brH60Store.Controllers
{
    public class ProductCategoriesController : Controller {
        private readonly IProductCategoryRepository _prodCatRepository;

        public ProductCategoriesController(IProductCategoryRepository prodCatRepository) {
           _prodCatRepository = prodCatRepository;
        }

        // GET: ProductCategories
        [HttpGet]
        [Authorize(Roles = "Manager,Clerk")]
        public async Task<IActionResult> Index() {
            List<ProductCategory> categories = await _prodCatRepository.GetProductCategories();
            return View(categories);
        }

        // GET: ProductCategories/Details/5
        [Authorize(Roles = "Manager,Clerk")]
        public async Task<IActionResult> Details(int id) {
            ProductCategory category = await _prodCatRepository.GetProductCategory(id);

            if (category == null) {
                return RedirectToAction("Index");
            }

            return View(category);
        }

        [HttpGet("CategroyProducts/{id}")]
        [Authorize(Roles = "Manager,Clerk")]
        public async Task<IActionResult> CategoryProducts(int id) {
            List<Product> products = await _prodCatRepository.GetCategoryProducts(id);

            return View(products);
        }

        [Authorize(Roles = "Manager,Clerk")]
        // GET: ProductCategories/Create
        public IActionResult Create() {
            return View();
        }

        //POST: ProductCategories/Create
        //To protect from overposting attacks, enable the specific properties you want to bind to.
        //For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager,Clerk")]
        public async Task<IActionResult> Create([Bind("CategoryId,ProdCat")] ProductCategory productCategory) {
            if (ModelState.IsValid) {
                bool ProdCaAdded = await _prodCatRepository.AddProductCategry(productCategory);

                if (ProdCaAdded) {
                    return RedirectToAction(nameof(Index));
                }
                return View(productCategory);
            }
            return View(productCategory);
        }

        //// GET: ProductCategories/Edit/5
        [HttpGet("ProductCategories/Edit/{id}")]
        [Authorize(Roles = "Manager,Clerk")]
        public async Task<IActionResult> Edit(int id) {
            ProductCategory category = await _prodCatRepository.GetProductCategory(id);

            return View(category);
        }

        //// POST: ProductCategories/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("ProductCategories/Edit/{id}")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager,Clerk")]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,ProdCat")] ProductCategory productCategory) {
            if (id != productCategory.CategoryId) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                bool prodCat = await _prodCatRepository.EditProductCategory(id, productCategory);

                if (prodCat) {
                    return RedirectToAction(nameof(Index));
                }
                return View(productCategory);
            }
            return View(productCategory);
        }

        //// GET: ProductCategories/Delete/5
        [HttpGet]
        [Authorize(Roles = "Manager,Clerk")]
        public async Task<IActionResult> Delete(int id) {
            ProductCategory category = await _prodCatRepository.GetProductCategory(id);

            return View(category);
        }

        //// POST: ProductCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager,Clerk")]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            bool prodCat = await _prodCatRepository.DeleteProductCategory(id);

            if (prodCat) {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
