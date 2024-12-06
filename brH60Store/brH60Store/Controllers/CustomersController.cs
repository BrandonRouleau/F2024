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

namespace brH60Store.Controllers {
    public class CustomersController : Controller {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository) {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Manager,Clerk")]
        public async Task<IActionResult> Index() {
            List<Customer> customers = await _customerRepository.GetCustomers();
            return View(customers);
        }

        [Authorize(Roles = "Manager,Clerk")]
        public async Task<IActionResult> Details(int id) {
            Customer customer = await _customerRepository.GetCustomer(id);

            if (customer == null) {
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        [HttpGet]
        [Authorize(Roles = "Manager,Clerk")]
        public async Task<IActionResult> Create() {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Manager,Clerk")]
        public async Task<IActionResult> Create([Bind("CustomerId, FirstName, LastName, Email, PhoneNumber, Province, CreditCard")] Customer customer) {
            if (ModelState.IsValid) {
                var cust = await _customerRepository.AddCustomer(customer);

                if (cust) {
                    return RedirectToAction(nameof(Index));
                }
                return View(customer);
            }

            return View(customer);
        }

        [HttpGet]
        [Authorize(Roles = "Manager,Clerk")]
        public async Task<IActionResult> Edit(int id) {
            if (id == null) return NotFound();

            //Get product from id
            Customer customer = await _customerRepository.GetCustomer(id);
            if (customer == null) return NotFound();

            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager,Clerk")]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId, FirstName, LastName, Email, PhoneNumber, Province, CreditCard")] Customer customer) {
            if (ModelState.IsValid) {
                var cust = await _customerRepository.EditCustomer(id, customer);

                if (cust) {
                    return RedirectToAction(nameof(Index));
                }
                return View(customer);
            }

            return View(customer);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int id) {
            if (id == null) return NotFound();

            Customer customer = await _customerRepository.GetCustomer(id);

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> ConfirmDelete(int id) {
            if (id == null) return NotFound();

            var cust = await _customerRepository.DeleteCustomer(id);

            if (cust) {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
