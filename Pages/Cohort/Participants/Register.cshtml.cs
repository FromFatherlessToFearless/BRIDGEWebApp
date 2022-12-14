using BRIDGEWebApp.Data.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using BRIDGEWebApp.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using BRIDGEWebApp.Utility;

namespace BRIDGEWebApp.Pages
{
    public class ParticipantRegisterModel : PageModel
    {
        private readonly BRIDGEWebApp.Data.ApplicationDbContext _context;

        public ParticipantRegisterModel(BRIDGEWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ParticipantRegisterViewModel Participant { get; set; }

        public bool IsAuthenticated { get; set; }

        public Data.Models.Cohort Cohort { get; set; }

        public async Task<IActionResult> OnGetAsync(int cohortId)
        {
            var auth = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            

            if (cohortId == null || _context.Cohorts == null)
            {
                return NotFound();
            }

            var cohort = await _context.Cohorts.FirstOrDefaultAsync(m => m.Id == cohortId);
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

            var cohort = await _context.Cohorts.FirstOrDefaultAsync(m => m.Id == cohortId);
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
                var participant = new Participant()
                {
                    FirstName = Participant.FirstName,
                    LastName = Participant.LastName,
                    CohortId = cohortId,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now,
                    Email = Participant.Email,
                    IsActive = true,
                    PhoneNumber = Participant.PhoneNumber,
                    PINHash = HashProvider.ComputeSha256Hash(Participant.PIN)
                            
                };

                _context.Participants.Add(participant);
                await _context.SaveChangesAsync();

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

                return RedirectToPage("Landing", routeValues: new { cohortId=  Cohort.Id });
            }

            // Something failed. Redisplay the form.
            return Page();
        }
    }
}