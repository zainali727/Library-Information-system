using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;

namespace WebApplication.Controllers
{
    [Authorize(Roles = "Administrator,Manager")]
    public class IssuedBooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IssuedBooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.IssuedBook.ToListAsync());
        }
    }
}