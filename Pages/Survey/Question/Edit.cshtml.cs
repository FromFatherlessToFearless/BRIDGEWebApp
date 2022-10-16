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
using BRIDGEWebApp.Data.Enum;
using Microsoft.AspNetCore.Authorization;

namespace BRIDGEWebApp.Pages.Survey.Question
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
        public Data.Models.Question Question { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int surveyId, int? id)
        {
            var surveySection = _context.SurveySections.ToList();
            var survey = _context.Surveys.FirstOrDefault(m => m.Id == surveyId);
            var field = new SelectList(surveySection, "Id", "Name");
            ViewData["SurveySectionId"] = new SelectList(surveySection, "Id", "Name");
            ViewData["SurveyId"] = surveyId;
            ViewData["SurveyName"] = survey.Name;
            var statuses = from QuestionTypeEnum.QuestionTypesEnum s in Enum.GetValues(typeof(QuestionTypeEnum.QuestionTypesEnum))
                           select new { ID = s, Name = s.ToString() };
            ViewData["QuestionTypeEnum"] = new SelectList(statuses, "ID", "Name");
            if (id == null || _context.Questions == null)
            {
                return NotFound();
            }

            var question =  await _context.Questions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }
            Question = question;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int surveyId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Question.UpdatedBy = userId;
            Question.UpdatedOn = DateTime.Now;
            ModelState.Remove("Question.Survey");
            ModelState.Remove("Question.SurveySection");
            ModelState.Remove("Question.Survey.CreatedBy");
            ModelState.Remove("Question.Survey.UpdatedBy");
            ModelState.Remove("Question.CreatedByIdentityUser");
            ModelState.Remove("Question.UpdatedByIdentityUser");
            ModelState.Remove("Question.Survey.CreatedByIdentityUser");
            ModelState.Remove("Question.Survey.UpdatedByIdentityUser");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Question).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(Question.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index", new { surveyId = Question.SurveyId });
        }

        private bool QuestionExists(int id)
        {
          return _context.Questions.Any(e => e.Id == id);
        }
    }
}
