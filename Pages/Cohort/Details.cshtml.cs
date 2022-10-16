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
using BRIDGEWebApp.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace BRIDGEWebApp.Pages.Cohort
{
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class DetailsModel : PageModel
    {
        private readonly BRIDGEWebApp.Data.ApplicationDbContext _context;

        public DetailsModel(BRIDGEWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public CohortParticipantListModel Cohort { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
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

                Cohort = _context.Cohorts.Include(c => c.Participants).Where(c => c.Id == id).Select(x => new CohortParticipantListModel()
                {
                    Id = x.Id,
                    Participants = x.Participants,
                    Description = x.Description,
                    IsRegistrationOpen = x.IsRegistrationOpen,
                    Name = x.Name,
                    StartDate = x.StartDate
                }).First();
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

            // Toggle the Registration
            cohort.IsRegistrationOpen = !cohort.IsRegistrationOpen;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            cohort.UpdatedBy = userId;
            cohort.UpdatedOn = DateTime.Now;

            _context.Attach(cohort).State = EntityState.Modified;

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

            if (cohort.IsRegistrationOpen)
            {
                return RedirectToPage("Code", new { cohortId = cohort.Id });
            }

            return Page();
        }

        private bool CohortExists(int id)
        {
            return _context.Cohorts.Any(e => e.Id == id);
        }
    }
}
