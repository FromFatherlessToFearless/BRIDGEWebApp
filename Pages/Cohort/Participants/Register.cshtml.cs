using BRIDGEWebApp.Data.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using BRIDGEWebApp.Data.ViewModels;

namespace BRIDGEWebApp.Pages
{
    public class ParticipantRegisterModel : PageModel
    {
        [BindProperty]
        public ParticipantRegisterViewModel Participant { get; set; }

        public void OnGet(int cohortId)
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //ReturnUrl = returnUrl;

            if (ModelState.IsValid)
            {
                // Use Input.Email and Input.Password to authenticate the user
                // with your custom authentication logic.
                //
                // For demonstration purposes, the sample validates the user
                // on the email address maria.rodriguez@contoso.com with
                // any password that passes model validation.

                // var user =

                if (false)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }

                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, Participant.FirstName),
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

                //_logger.LogInformation("User {Email} logged in at {Time}.",
                //    user.Email, DateTime.UtcNow);

                return LocalRedirect(Url.Page("Survey"));
            }

            // Something failed. Redisplay the form.
            return Page();
        }
    }
}