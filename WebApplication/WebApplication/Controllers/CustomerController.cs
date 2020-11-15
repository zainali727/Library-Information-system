using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Data;

namespace WebApplication.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET
        public IActionResult Index()
        {
            var customers = _context.Customers.ToList();
            return View(customers);
        }

        [HttpPost]
        public IActionResult AddOrEdit()
        {
            var customers = _context.Customers.ToList();
            return View("Index", customers);
        }

        [HttpPost]
        public IActionResult Delete()
        {
            var customers = _context.Customers.ToList();
            return View("Index", customers);
        }
    }
}