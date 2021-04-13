using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LibrarySystem.Data;
using LibrarySystem.Domain;
using LibrarySystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LibrarySystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<HomeController> logger)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var issuedBooks = new List<IssuedBookModel>();
            
            if (User.Identity.IsAuthenticated)
            {
                var user = _userManager.Users.First(x => x.UserName == User.Identity.Name);
                var issuedBooksDb = (await _context
                        .IssuedBook
                        .Include(x => x.Member)
                        .Include(x => x.Book)
                        .ToListAsync())
                    .Select(x =>
                    {
                        var overdueDays = Convert.ToInt32(Math.Ceiling((DateTime.UtcNow - x.ReturnDate).TotalDays));
                        return new IssuedBookModel
                        {
                            Id = x.Id,
                            Title = x.Book.Title,
                            ISBN = x.Book.ISBN,
                            IssuedTo = new MemberModel {Id = x.Member.Id, Email = x.Member.Email},
                            IssuedDate = x.IssuedDate,
                            ReturnDate = x.ReturnDate,
                            CalculatedFine = overdueDays > 0 ? overdueDays * x.Book.FinePerDay : 0m
                        };
                    });
                
                if (await _userManager.IsInRoleAsync(user, "Administrator") || await _userManager.IsInRoleAsync(user, "Manager"))
                {
                    issuedBooks.AddRange(issuedBooksDb.Where(x => x.ReturnDate.Date < DateTime.UtcNow.Date).OrderBy(x => x.ReturnDate));   
                }
                else
                {
                    issuedBooks.AddRange(issuedBooksDb.Where(x => x.IssuedTo.Email == user.Email).OrderByDescending(x => x.CalculatedFine));
                }
            }
            
            return View(issuedBooks);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
        
        public IActionResult Banned()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> MyDetails()
        {
            await ImportMembersAsync();
            
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(Index));
            }
            
            var member = await _context.Members.SingleOrDefaultAsync(x => x.Email == User.Identity.Name);

            if (member != null)
            {
                return View(member);
            }

            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MyDetails(Member member)
        {
            if (ModelState.IsValid)
            {
                _context.Update(member);
                
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            return View(member);
        }
        
        private async Task<IActionResult> ImportMembersAsync()
        {
            var users = _userManager.Users.ToList();

            foreach (var user in users)
            {
                var currentMember = _context
                    .Members
                    .FirstOrDefault(x => x.Email.ToLower() == user.UserName.ToLower());
                
                if(currentMember != null) continue;
                await _userManager.AddToRoleAsync(user, "User");
                if(await _userManager.IsInRoleAsync(user, "Administrator") || await _userManager.IsInRoleAsync(user, "Manager")) continue;

                var member = new Member
                {
                    Firstname = "", Lastname = "", PostCode = "",
                    Email = user.UserName,
                    AddressLine1 = "", AddressLine2 = "", AddressLine3 = "", City = "", County = "", Telephone = ""
                };

                await _context.AddAsync(member);
            }
            
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}