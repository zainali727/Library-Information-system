using System;
using System.Linq;
using System.Threading.Tasks;
using LibrarySystem.Data;
using LibrarySystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Controllers
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
            var issuedBooks = (await _context
                .IssuedBook
                .Include(x => x.Member)
                .Include(x => x.Book)
                .ToListAsync())
                .Select(x =>
                {
                    //calculate fine function
                    // 
                    var overdueDays = Convert.ToInt32(Math.Ceiling((DateTime.UtcNow - x.ReturnDate).TotalDays));
                    return new IssuedBookModel
                    {
                        Id = x.Id,
                        Title = x.Book.Title,
                        ISBN = x.Book.ISBN,
                        IssuedTo = new MemberModel {Id = x.Member.Id, Email = x.Member.Email},
                        IssuedDate = x.IssuedDate,
                        ReturnDate = x.ReturnDate,
                        //if the overdue days are more than 0 multiply it by the fine
                        //if its not overdue the calculation takes place but appears as 0
                        // not linked to db just calculated on the db
                        CalculatedFine = overdueDays > 0 ? overdueDays * x.Book.FinePerDay : 0m
                    };
                });
            
            return View(issuedBooks.OrderBy(x => x.ReturnDate));
        }
        
        // return book method. FInd the book from the data, if its not null remove it from the db
        public async Task<IActionResult> ReturnBook(int? id)
        {
            var issuedBook = _context.IssuedBook.Include(x => x.Book).FirstOrDefault(x => x.Id == id);

            if (issuedBook != null)
            {
                // ++ means the stock level is increased (stock)
                _context.IssuedBook.Remove(issuedBook);
                issuedBook.Book.Quantity++;
                
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}