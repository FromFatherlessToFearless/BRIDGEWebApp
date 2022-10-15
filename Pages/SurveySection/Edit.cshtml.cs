using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BRIDGEWebApp.Data;
using BRIDGEWebApp.Data.Models;
using System.Security.Claims;

namespace BRIDGEWebApp.Pages.SurveySection
{
    public class EditModel : PageModel
    {
        private readonly BRIDGEWebApp.Data.ApplicationDbContext _context;

        public EditModel(BRIDGEWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Data.Models.SurveySection SurveySection { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.SurveySections == null)
            {
                return NotFound();
            }

            var surveysection =  await _context.SurveySections.FirstOrDefaultAsync(m => m.Id == id);
            if (surveysection == null)
            {
                return NotFound();
            }
            SurveySection = surveysection;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            SurveySection.UpdatedBy = userId;
            SurveySection.UpdatedOn = DateTime.Now;

            ModelState.Remove("SurveySection.CreatedByIdentityUser");
            ModelState.Remove("SurveySection.UpdatedByIdentityUser");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(SurveySection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SurveySectionExists(SurveySection.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SurveySectionExists(int id)
        {
          return _context.SurveySections.Any(e => e.Id == id);
        }
    }
}
