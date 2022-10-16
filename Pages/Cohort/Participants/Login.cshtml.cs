using BRIDGEWebApp.Data.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using BRIDGEWebApp.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using BRIDGEWebApp.Utility;

namespace BRIDGEWebApp.Pages
{
    public class ParticipantLoginModel : PageModel
    {
        private readonly BRIDGEWebApp.Data.ApplicationDbContext _context;

        public ParticipantLoginModel(BRIDGEWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ParticipantLoginViewModel Participant { get; set; }

        public Data.Models.Cohort Cohort { get; set; }

        public async Task<IActionResult> OnGetAsync(int cohortId)
        {

            if (cohortId == null || _context.Cohorts == null)
            {
                return NotFound();
            }

            var cohort = await _context.Cohorts.Include(c => c.Participants).FirstOrDefaultAsync(m => m.Id == cohortId);
            if (cohort == null)
            {
                return NotFound();
            }
            else
            {
                Cohort = cohort;
            }


            // TODO: Redirect them if registration is closed
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int cohortId)
        {
            //ReturnUrl = returnUrl;
            if (cohortId == null || _context.Cohorts == null)
            {
                return NotFound();
            }

            var cohort = await _context.Cohorts.Include(c => c.Participants).FirstOrDefaultAsync(m => m.Id == cohortId);
            if (cohort == null)
            {
                return NotFound();
            }
            else
            {
                Cohort = cohort;
            }

            if (ModelState.IsValid)
            {
                // Use Input.Email and Input.Password to authenticate the user
                // with your custom authentication logic.
                //
                // For demonstration purposes, the sample validates the user
                // on the email address maria.rodriguez@contoso.com with
                // any password that passes model validation.

                var user = _context.Participants.Where(x => x.FirstName.Trim().ToLower() == Participant.FirstName.Trim().ToLower()
                                                         && x.LastName.Trim().ToLower() == Participant.LastName.Trim().ToLower()
                                                         && x.CohortId == Cohort.Id).FirstOrDefault();
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }

                var providedPasswordHash = HashProvider.ComputeSha256Hash(Participant.PIN.Trim());

                if (providedPasswordHash.CompareTo(user.PINHash) != 0)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }

                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, $"{Participant.FirstName} {Participant.LastName}"),
                        new Claim("CohortId", Cohort.Id.ToString())
                    };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    // The time at which the authentication ticket expires. A
                    // value set here overrides the ExpireTimeSpan option of
                    // CookieAuthenticationOptions set with AddCookie.

                    //IsPersistent = true,
                    // Whether the authentication session is persisted across
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http
                    // redirect response value.
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToPage("Landing", routeValues: new { cohortId = Cohort.Id });
            }

            // Something failed. Redisplay the form.
            return Page();
        }
    }
}