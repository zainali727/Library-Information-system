using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Models;

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
            var issuedBooks = (await _context
                .IssuedBook
                .ToListAsync())
                .Select(x =>
                {
                    var overdueDays = Convert.ToInt32(Math.Ceiling((DateTime.UtcNow - x.ReturnDate).TotalDays));
                    return new IssuedBookModel
                    {
                        Title = x.Book.Title,
                        ISBN = x.Book.ISBN,
                        IssuedTo = new MemberModel {Id = x.Member.Id, Email = x.Member.Email},
                        IssuedDate = x.IssuedDate,
                        ReturnDate = x.ReturnDate,
                        CalculatedFine = overdueDays > 0 ? overdueDays * x.Book.FinePerDay : 0m
                    };
                });
            
            return View(issuedBooks);
        }
    }
}