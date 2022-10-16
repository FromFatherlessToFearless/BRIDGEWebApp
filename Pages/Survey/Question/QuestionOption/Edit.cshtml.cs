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
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BRIDGEWebApp.Pages.Survey.Question.QuestionOption
{
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class EditModel : PageModel
    {
        private readonly BRIDGEWebApp.Data.ApplicationDbContext _context;

        public EditModel(BRIDGEWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Data.Models.QuestionOption QuestionOption { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int surveyId, int questionId, int? id)
        {
            if (id == null || _context.QuestionOptions == null)
            {
                return NotFound();
            }
            ViewData["SurveyId"] = surveyId;
            ViewData["QuestionId"] = questionId;
            var questionoption =  await _context.QuestionOptions.FirstOrDefaultAsync(m => m.Id == id);
            if (questionoption == null)
            {
                return NotFound();
            }
            QuestionOption = questionoption;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int surveyId, int questionId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            QuestionOption.UpdatedBy = userId;
            QuestionOption.UpdatedOn = DateTime.Now;
            ModelState.Remove("QuestionOption.CreatedByIdentityUser");
            ModelState.Remove("QuestionOption.UpdatedByIdentityUser");
            ModelState.Remove("QuestionOption.CreatedBy");
            ModelState.Remove("QuestionOption.UpdatedBy");
            ModelState.Remove("QuestionOption.Question");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(QuestionOption).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionOptionExists(QuestionOption.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index", new { surveyId = surveyId, questionId = QuestionOption.QuestionId  });
        }

        private bool QuestionOptionExists(int id)
        {
          return _context.QuestionOptions.Any(e => e.Id == id);
        }
    }
}
