using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BRIDGEWebApp.Data;
using BRIDGEWebApp.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace BRIDGEWebApp.Pages.Survey.Question
{
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class DetailsModel : PageModel
    {
        private readonly BRIDGEWebApp.Data.ApplicationDbContext _context;

        public DetailsModel(BRIDGEWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Data.Models.Question Question { get; set; }

        public async Task<IActionResult> OnGetAsync(int surveyId, int? id)
        {
            if (id == null || _context.Questions == null)
            {
                return NotFound();
            }
            var survey = _context.Surveys.FirstOrDefault(m => m.Id == surveyId);
            ViewData["SurveyId"] = surveyId;
            ViewData["SurveyName"] = survey.Name;
            var question = await _context.Questions
                .Include(s => s.SurveySection)
                .Include(s => s.Survey)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }
            else 
            {
                Question = question;
            }
            return Page();
        }
    }
}
