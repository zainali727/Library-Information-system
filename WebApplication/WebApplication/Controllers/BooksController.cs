using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Domain;

namespace WebApplication.Controllers
{
    [Authorize(Roles = "Administrator,Manager")]
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public BooksController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET
        public async Task<IActionResult> Index(string searchString)
        {
            var books = from m in _context.Books.Include(x => x.BookReviews)
                        select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title.Contains(searchString) || s.Author.Contains(searchString) || s.Genre.Contains(searchString));

            }

            return View(await books.ToListAsync());
        }

        public IActionResult Show(int id)
        {
            return View(_context.Books.Include(x => x.BookReviews).FirstOrDefault(x => x.Id == id));
        }
        
        // GET: Member/Create
        public IActionResult AddOrEdit(int id = 0)
        {
            return View(id == 0
                ? new Book { PublishedDate = DateTime.Now}
                : _context.Books.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(Book book)
        {
            if (ModelState.IsValid)
            {    
                if (book.MyImage != null)
                {
                    // var uniqueFileName = GetUniqueFileName(book.MyImage.FileName);
                    // var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                    // var filePath = Path.Combine(uploads,uniqueFileName);
                    // await book.MyImage.CopyToAsync(new FileStream(filePath, FileMode.OpenOrCreate));
                    // book.ImageFileName = uniqueFileName;

                    await using var ms = new MemoryStream();
                    await book.MyImage.CopyToAsync(ms);
                    book.ImageBytes = ms.ToArray();
                }
                
                if (book.Id == 0)
                    _context.Add(book);
                else
                    _context.Update(book);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var book = _context.Books.Include(x => x.BookReviews).FirstOrDefault(x => x.Id == id);

            if (book != null)
            {
                var bookReviews = book.BookReviews;
                foreach (var bookReview in bookReviews)
                {
                    _context.BookReviews.Remove(bookReview);
                }

                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
        
        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return  Path.GetFileNameWithoutExtension(fileName)
                    + "_" 
                    + Guid.NewGuid().ToString().Substring(0, 4) 
                    + Path.GetExtension(fileName);
        }

       
    }
}