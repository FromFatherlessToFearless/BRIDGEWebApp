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

namespace BRIDGEWebApp.Pages.Survey
{
    public class EditModel : PageModel
    {
        private readonly BRIDGEWebApp.Data.ApplicationDbContext _context;

        public EditModel(BRIDGEWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Data.Models.Survey Survey { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Surveys == null)
            {
                return NotFound();
            }

            var survey =  await _context.Surveys.FirstOrDefaultAsync(m => m.Id == id);
            if (survey == null)
            {
                return NotFound();
            }
            Survey = survey;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Survey.UpdatedBy = userId;
            Survey.UpdatedOn = DateTime.Now;
            ModelState.Remove("Survey.CreatedByIdentityUser");
            ModelState.Remove("Survey.UpdatedByIdentityUser");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Survey).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SurveyExists(Survey.Id))
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

        private bool SurveyExists(int id)
        {
          return _context.Surveys.Any(e => e.Id == id);
        }
    }
}
