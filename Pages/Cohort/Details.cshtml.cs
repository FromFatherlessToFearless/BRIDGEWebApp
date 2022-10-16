using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BRIDGEWebApp.Data;
using BRIDGEWebApp.Data.Models;
using System.Security.Claims;

namespace BRIDGEWebApp.Pages.Cohort
{
    public class DetailsModel : PageModel
    {
        private readonly BRIDGEWebApp.Data.ApplicationDbContext _context;

        public DetailsModel(BRIDGEWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Data.Models.Cohort Cohort { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Cohorts == null)
            {
                return NotFound();
            }

            var cohort = await _context.Cohorts.Include(c => c.Participants).FirstOrDefaultAsync(m => m.Id == id);
            if (cohort == null)
            {
                return NotFound();
            }
            else 
            {
                Cohort = cohort;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostToggleRegistrationAsync(int? id)
        {
            if (id == null || _context.Cohorts == null)
            {
                return NotFound();
            }

            var cohort = await _context.Cohorts.FirstOrDefaultAsync(m => m.Id == id);
            if (cohort == null)
            {
                return NotFound();
            }
            else
            {
                Cohort = cohort;
            }

            // Toggle the Registration
            Cohort.IsRegistrationOpen = !Cohort.IsRegistrationOpen;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Cohort.UpdatedBy = userId;
            Cohort.UpdatedOn = DateTime.Now;

            _context.Attach(Cohort).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CohortExists(Cohort.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            if (Cohort.IsRegistrationOpen)
            {
                return RedirectToPage("Code", new { cohortId = Cohort.Id });
            }

            return Page();
        }

        private bool CohortExists(int id)
        {
            return _context.Cohorts.Any(e => e.Id == id);
        }
    }
}
