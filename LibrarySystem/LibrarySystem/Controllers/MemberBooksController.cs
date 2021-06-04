using System;
using System.Linq;
using System.Threading.Tasks;
using LibrarySystem.Data;
using LibrarySystem.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Controllers
{
    public class MemberBooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public MemberBooksController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var books = from m in _context.Books.Include(x => x.BookReviews)
                select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title.Contains(searchString) || s.Author.Contains(searchString) || s.Genre.Contains(searchString));
            }

            return View(await books.ToListAsync());
            
            
        }

        public IActionResult Show(int id)
        {
            var firstOrDefault = _context.Books.Include(x => x.BookReviews).FirstOrDefault(x => x.Id == id);
            return View(firstOrDefault);
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(int id, BookReview bookReview)
        {
            if (ModelState.IsValid)
            {
                var book = _context.Books.Include(x => x.BookReviews).FirstOrDefault(x => x.Id == id);
                
                if (book != null)
                {
                    book.BookReviews.Add(new BookReview
                    {
                        CreatedDate = DateTime.UtcNow,
                        ReviewerName = bookReview.ReviewerName,
                        Text = bookReview.Text
                    });

                    _context.Update(book);

                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToRoute(new
            {
                controller = "MemberBooks",
                action = "Show",
                id = id
            });
        }
    }
}