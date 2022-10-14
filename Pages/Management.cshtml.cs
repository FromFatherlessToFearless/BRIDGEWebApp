using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BRIDGEWebApp.Pages
{
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class ManagementModel : PageModel
    {
        public void OnGet()
        {
            var claims = User.Claims.ToList();
        }
    }
}