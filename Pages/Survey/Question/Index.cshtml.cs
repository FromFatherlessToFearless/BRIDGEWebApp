using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BRIDGEWebApp.Data;
using BRIDGEWebApp.Data.Models;
using BRIDGEWebApp.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace BRIDGEWebApp.Pages.Survey.Question
{
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class IndexModel : PageModel
    {
        private readonly BRIDGEWebApp.Data.ApplicationDbContext _context;

        public IndexModel(BRIDGEWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
            QuestionList = new QuestionListViewModel();
        }

        public QuestionListViewModel QuestionList { get; set; } = default!;
        public async Task OnGetAsync(int surveyId)
        {
            if (_context.Questions != null)
            {
                //this is needed when there are no questions for a survey
                var survey = await _context.Surveys.FirstOrDefaultAsync(m => m.Id == surveyId);
                QuestionList.QuestionViewModels = await _context.Questions
                .Include(s => s.CreatedByIdentityUser)
                .Include(s => s.UpdatedByIdentityUser)
                .Include(s => s.SurveySection)
                .Include(s => s.Survey)
                .Select(s => new QuestionViewModel
                {
                    Id = s.Id,
                    IsActive = s.IsActive,
                    Text = s.Text,
                    CreatedOn = s.CreatedOn,
                    UpdatedOn = s.UpdatedOn,
                    SurveyId = surveyId,
                    SurveyName = s.Survey.Name,
                    QuestionType = s.QuestionType,
                    Order = s.Order,
                    IsMandatory = s.IsMandatory,
                    SurveySectionId = s.SurveySectionId,
                    SurveySectionName = s.SurveySection.Name
                }).OrderBy(s => s.SurveySectionName).ThenBy(s => s.Order)
                .ToListAsync();
                QuestionList.SurveyId = survey.Id;
                QuestionList.SurveyName = survey.Name;
            }
        }
    }
}
