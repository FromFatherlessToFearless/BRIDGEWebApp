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
using BRIDGEWebApp.Data.ViewModels;

namespace BRIDGEWebApp.Pages.Survey.Question.QuestionOption
{
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class IndexModel : PageModel
    {
        private readonly BRIDGEWebApp.Data.ApplicationDbContext _context;

        public IndexModel(BRIDGEWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
            QuestionOptionList = new QuestionOptionListViewModel();
        }

        public QuestionOptionListViewModel QuestionOptionList { get;set; } = default!;

        public async Task OnGetAsync(int surveyId, int questionId)
        {
            if (_context.QuestionOptions != null)
            {
                var question = _context.Questions.FirstOrDefault(m => m.Id == questionId);
                QuestionOptionList.QuestionOptionViewModels = await _context.QuestionOptions
                .Include(q => q.CreatedByIdentityUser)
                .Include(q => q.Question)
                .Include(q => q.UpdatedByIdentityUser).Select(s => new QuestionOptionViewModel
                {
                    Id = s.Id,
                    IsActive = s.IsActive,
                    Text = s.Text,
                    CreatedOn = s.CreatedOn,
                    UpdatedOn = s.UpdatedOn,
                    IsOther = s.IsOther,
                    QuestionId = questionId,
                    Order = s.Order
                }).OrderBy(s => s.Order)
                .ToListAsync();
                QuestionOptionList.QuestionId = questionId;
                QuestionOptionList.SurveyId = surveyId;
            }
        }
    }
}
