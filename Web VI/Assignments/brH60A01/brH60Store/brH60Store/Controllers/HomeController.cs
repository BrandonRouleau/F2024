using brH60Store.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace brH60Store.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly H60assignmentDbBrContext _context;

        public HomeController(ILogger<HomeController> logger, H60assignmentDbBrContext context) {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index() {
            return View(await _context.ProductCategories.ToListAsync());
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
