using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Data;
using WebApplication.Domain;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Authorize(Roles = "Administrator,Manager")]
    public class MemberController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public MemberController(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET
        public async Task<IActionResult> Index()
        {
            await ImportMembers();
            var members = _context.Members?.ToList();
            return View(members);
        }

        public IActionResult Show(int id)
        {
            return View(_context.Members.Find(id));
        }
        
        // GET: Members/Create
        public IActionResult AddOrEdit(int id = 0)
        {
            return View(id == 0 
                ? new Member() 
                : _context.Members.Find(id));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(Member member)
        {
            if (ModelState.IsValid)
            {
                if (member.Id == 0)
                    _context.Add(member);
                else
                    _context.Update(member);
                
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var members = await _context.Members.FindAsync(id);

            if (members != null)
            {
                _context.Members.Remove(members);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> LockoutMember(Member member)
        {
            var findMember = await _context.Members.FindAsync(member.Id);

            if (findMember != null)
            {
                findMember.Banned = !member.Banned;
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        public async Task<IActionResult> ImportMembers()
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

        [HttpGet]
        public IActionResult MemberIssueBook(int id, string searchString)
        {
            var model = new MemberIssueBookModel();
            var member = _context.Members.Find(id);
            model.MemberId = member.Id;
            model.Firstname = member.Firstname;
            model.Lastname = member.Lastname;
            model.Email = member.Email;
            model.BookFound = true;

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                model.Book = _context.Books.SingleOrDefault(x => x.ISBN.ToLower() == searchString.ToLower());
                if (model.Book != null)
                {
                    model.BookFound = true;
                    model.BookId = model.Book.Id;
                }
            }

            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MemberIssueBook(MemberIssueBookModel model)
        {
            var member = await _context.Members.FindAsync(model.MemberId);
            var book = await _context.Books.FindAsync(model.BookId);

            var issuedBook = new IssuedBook
            {
                Book = book, Member = member, IssuedDate = DateTime.UtcNow, ReturnDate = DateTime.UtcNow.AddDays(10)
            };

            _context.IssuedBook.Add(issuedBook);

            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}