using BRIDGEWebApp.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Security.Claims;

namespace BRIDGEWebApp.Pages.Cohort
{
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class CreateModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public CreateModel(BRIDGEWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            // To default it to today or later
            Cohort.StartDate = DateTime.Now;
            return Page();
        }

        [BindProperty]
        public CohortViewModel Cohort { get; set; } = new CohortViewModel();
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Data.Models.Cohort cohort = new Data.Models.Cohort();
            cohort.UpdatedBy = userId;
            cohort.CreatedBy = userId;
            cohort.UpdatedOn = DateTime.Now;
            cohort.CreatedOn = DateTime.Now;
            cohort.Description = Cohort.Description;
            cohort.IsActive = true;
            cohort.IsRegistrationOpen = false;
            cohort.Name = Cohort.Name;
            cohort.StartDate = Cohort.StartDate;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Cohorts.Add(cohort);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
