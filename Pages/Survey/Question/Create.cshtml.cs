using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BRIDGEWebApp.Data;
using BRIDGEWebApp.Data.Models;
using BRIDGEWebApp.Data.ViewModels;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using BRIDGEWebApp.Data.Enum;
using Microsoft.AspNetCore.Authorization;

namespace BRIDGEWebApp.Pages.Survey.Question
{
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class CreateModel : PageModel
    {
        private readonly BRIDGEWebApp.Data.ApplicationDbContext _context;

        public CreateModel(BRIDGEWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int surveyId)
        {
            var surveySection = _context.SurveySections.ToList();
            ViewData["SurveySectionId"] = new SelectList(surveySection, "Id", "Name");
            var statuses = from QuestionType.QuestionTypes s in Enum.GetValues(typeof(QuestionType.QuestionTypes))
                           select new { ID = s, Name = s.ToString() };
            ViewData["QuestionTypeEnum"] = new SelectList(statuses, "ID", "Name");
            ViewData["SurveyId"] = surveyId;
            return Page();
        }

        [BindProperty]
        public QuestionViewModel Question { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int surveyId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Data.Models.Question question = new Data.Models.Question();
            question.UpdatedBy = userId;
            question.CreatedBy = userId;
            question.UpdatedOn = DateTime.Now;
            question.CreatedOn = DateTime.Now;
            question.Text = Question.Text;
            question.IsMandatory = Question.IsMandatory;
            question.Order = Question.Order;
            question.QuestionType = Question.QuestionType;
            question.SurveyId = surveyId;
            question.SurveySectionId = Question.SurveySectionId;
            question.IsActive = true;
            ModelState.Remove("Question.CreatedBy");
            ModelState.Remove("Question.UpdatedBy");
            ModelState.Remove("Question.SurveyName");
            ModelState.Remove("Question.SurveySectionName");
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index", new { surveyId = question.SurveyId });
        }
    }
}
