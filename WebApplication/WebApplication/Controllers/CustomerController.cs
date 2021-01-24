using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Index(string searchString)
        {
            var customer = from m in _context.Customers
                        select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                customer = customer.Where(s => s.Firstname.Contains(searchString) || s.Lastname.Contains(searchString) || s.PostCode.Contains(searchString));

            }

            return View(await customer.ToListAsync());
        }

        public IActionResult Show(int id)
        {
            return View(_context.Customers.Find(id));
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

        public async Task<IActionResult> Delete(int? id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}