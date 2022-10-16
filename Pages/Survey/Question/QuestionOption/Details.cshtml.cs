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

namespace BRIDGEWebApp.Pages.Survey.Question.QuestionOption
{
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class DetailsModel : PageModel
    {
        private readonly BRIDGEWebApp.Data.ApplicationDbContext _context;

        public DetailsModel(BRIDGEWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Data.Models.QuestionOption QuestionOption { get; set; }

        public async Task<IActionResult> OnGetAsync(int surveyId, int questionId, int? id)
        {
            if (id == null || _context.QuestionOptions == null)
            {
                return NotFound();
            }
            ViewData["SurveyId"] = surveyId;
            ViewData["QuestionId"] = questionId;
            var questionoption = await _context.QuestionOptions.FirstOrDefaultAsync(m => m.Id == id);
            if (questionoption == null)
            {
                return NotFound();
            }
            else 
            {
                QuestionOption = questionoption;
            }
            return Page();
        }
    }
}
