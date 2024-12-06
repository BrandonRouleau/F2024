using brH60Store.DAL;
using brH60Store.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace brH60Store.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public HomeController(ILogger<HomeController> logger, IProductCategoryRepository productCategory) {
            _logger = logger;
            _productCategoryRepository = productCategory;
        }

        [Authorize(Roles = "Manager,Clerk")]
        public async Task<IActionResult> Index() {
            List <ProductCategory> categories = await _productCategoryRepository.GetProductCategories();
            return View(categories);
        }

        [Authorize(Roles = "Manager,Clerk")]
        public IActionResult Privacy() {
            return View();
        }

        [Authorize(Roles = "Manager,Clerk")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
