using System.Linq;
using LibrarySystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LibrarySystem.Filters
{
    // action filter piece of code that will run every time an action is carried out
    //specific to asp.net
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
                // if their email doesn't match carry on as normal
            if(currentMember == null || !currentMember.Banned) return;
            // if their email matches redirect them to the ban page
            context.Result = new RedirectToActionResult("Banned", "Home", null);
            
            base.OnResultExecuting(context);
        }
    }
}