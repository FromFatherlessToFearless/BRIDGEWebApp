using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BRIDGEWebApp.Data;
using BRIDGEWebApp.Data.Models;
using BRIDGEWebApp.Data.ViewModels.Survey;
using BRIDGEWebApp.Data.Enum;

namespace BRIDGEWebApp.Pages.Cohort.Participants.Survey
{
    public class IndexModel : PageModel
    {
        private readonly BRIDGEWebApp.Data.ApplicationDbContext _context;

        public IndexModel(BRIDGEWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public ParticipantSurveyViewModel Survey { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int cohortId)
        {
            if (cohortId == null || _context.Cohorts == null)
            {
                return NotFound();
            }

            var cohort = await _context.Cohorts.Include(c => c.Participants).FirstOrDefaultAsync(m => m.Id == cohortId);
            if (cohort == null)
            {
                return NotFound();
            }
            else
            {
                // TODO: Changed this to be dynamic
                Survey = await _context.Surveys.Where(s => s.Id == 1)
                    .Include(s => s.Questions)
                    .ThenInclude(s => s.QuestionOptions)
                    .Select(survey => new ParticipantSurveyViewModel()
                    {
                        CohortId = cohort.Id,
                        CohortName = cohort.Name,
                        SurveyId = survey.Id,
                        SurveyName = survey.Name,
                        ParticipantSurveySections = _context.SurveySections.Select(section => new ParticipantSurveySectionViewModel()
                        {
                            Order = section.Order,
                            SectionDescription = section.Description,
                            SectionId = section.Id,
                            SectionName = section.Name,
                            Questions = survey.Questions.Where(q => q.SurveyId == survey.Id).Select(q => new ParticipantSurveyQuestionViewModel()
                            {
                                IsMandatory = q.IsMandatory,
                                QuestionOptions = q.QuestionOptions.ToList(),
                                QuestionId = q.Id,
                                QuestionText = q.Text,
                                QuestionType = q.QuestionType,
                                Order = q.Order

                            }).ToList()

                        }).ToList()
                    })
                    .FirstOrDefaultAsync();

              





            }

            return Page();
        }
    }
}
