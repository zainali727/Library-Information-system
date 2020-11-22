using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Data;
using WebApplication.Models;

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

        // GET: Customer/Create
        public IActionResult AddOrEdit(int id = 0)
        {
            return View(id == 0 
                ? new Customer() 
                : _context.Customers.Find(id));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (customer.Id == 0)
                    _context.Add(customer);
                else
                    _context.Update(customer);
                
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        [HttpPost]
        public IActionResult Delete()
        {
            var customers = _context.Customers.ToList();
            return View("Index", customers);
        }
    }
}