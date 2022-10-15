using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BRIDGEWebApp.Pages.Cohort.Participants
{
    public class LandingModel : PageModel
    {
        private readonly BRIDGEWebApp.Data.ApplicationDbContext _context;

        public LandingModel(BRIDGEWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public Data.Models.Cohort Cohort { get; set; }

        public async Task<IActionResult> OnGetAsync(int cohortId)
        {

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
    }
}
