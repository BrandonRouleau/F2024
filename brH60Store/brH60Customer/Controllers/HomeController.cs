using brH60Customer.DAL;
using brH60Customer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;

namespace brH60Customer.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public HomeController(ILogger<HomeController> logger, IProductCategoryRepository productCategory) {
            _logger = logger;
            _productCategoryRepository = productCategory;
        }

        public async Task<IActionResult> Index() {
            List<ProductCategory> categories = await _productCategoryRepository.GetProductCategories();
            return View(categories);
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
