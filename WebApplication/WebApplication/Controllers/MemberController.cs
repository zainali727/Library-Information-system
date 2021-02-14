using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Data;
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
        public IActionResult Index()
        {
            var members = _context.Members.ToList();
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
            var users = _userManager.Users.ToList();
            
            return RedirectToAction(nameof(Index));
        }
    }
}