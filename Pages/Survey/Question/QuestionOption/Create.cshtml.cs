using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BRIDGEWebApp.Data;
using BRIDGEWebApp.Data.Models;
using Microsoft.AspNetCore.Authorization;
using BRIDGEWebApp.Data.ViewModels;
using System.Security.Claims;

namespace BRIDGEWebApp.Pages.Survey.Question.QuestionOption
{
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class CreateModel : PageModel
    {
        private readonly BRIDGEWebApp.Data.ApplicationDbContext _context;

        public CreateModel(BRIDGEWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int surveyId, int questionId)
        {
            ViewData["SurveyId"] = surveyId;
            ViewData["QuestionId"] = questionId;
            return Page();
        }

        [BindProperty]
        public QuestionOptionViewModel QuestionOption { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int surveyId, int questionId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Data.Models.QuestionOption questionOption = new Data.Models.QuestionOption();
            questionOption.UpdatedBy = userId;
            questionOption.CreatedBy = userId;
            questionOption.UpdatedOn = DateTime.Now;
            questionOption.CreatedOn = DateTime.Now;
            questionOption.Text = QuestionOption.Text;
            questionOption.IsOther = QuestionOption.IsOther;
            questionOption.Order = QuestionOption.Order;
            questionOption.QuestionId = questionId;
            questionOption.IsActive = true;
            ModelState.Remove("QuestionOption.CreatedBy");
            ModelState.Remove("QuestionOption.UpdatedBy");

            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.QuestionOptions.Add(questionOption);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { surveyId = surveyId, questionId = questionOption.QuestionId });
        }
    }
}
