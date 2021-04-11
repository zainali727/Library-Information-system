using System.Linq;
using LibrarySystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LibrarySystem.Filters
{
    public class BannedActionFilterAttribute : ActionFilterAttribute
    {
        private readonly ApplicationDbContext _context;

        public BannedActionFilterAttribute(ApplicationDbContext context)
        {
            _context = context;
        }
            
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if(context.ActionDescriptor.RouteValues.Values.Contains("Banned")) return;
            
            var identityName = context.HttpContext.User.Identity.Name;
            
            if(identityName == null) return;
            
            var currentMember = _context
                .Members
                .FirstOrDefault(x => x.Email.ToLower() == identityName.ToLower());
                
            if(currentMember == null || !currentMember.Banned) return;

            context.Result = new RedirectToActionResult("Banned", "Home", null);
            
            base.OnResultExecuting(context);
        }
    }
}