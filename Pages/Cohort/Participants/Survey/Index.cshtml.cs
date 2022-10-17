using BRIDGEWebApp.Data.Enum;
using BRIDGEWebApp.Data.Models;
using BRIDGEWebApp.Data.ViewModels.Survey;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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
                var list = _context.SurveySections.Include(sec => sec.Questions).ToList()
                        // Only pick the survey sections that are used in the questions tied to this survey
                        .Where(s => s.Questions.Where(q => q.SurveyId == 1).ToList().Count() > 0).ToList();
                // TODO: Changed this to be dynamic
                Survey = (await _context.Surveys.Where(s => s.Id == 1).ToListAsync())
                    .Select(survey => new ParticipantSurveyViewModel()
                    {
                        CohortId = cohort.Id,
                        CohortName = cohort.Name,
                        SurveyId = survey.Id,
                        SurveyName = survey.Name,
                        ParticipantSurveySections = _context.SurveySections.Include(sec => sec.Questions).ThenInclude(q => q.QuestionOptions).ToList()
                        // Only pick the survey sections that are used in the questions tied to this survey
                        .Where(s => s.Questions.Where(q => q.SurveyId == 1).Count() > 0)
                        .Select(section => new ParticipantSurveySectionViewModel()
                        {
                            Order = section.Order,
                            SectionDescription = section.Description,
                            SectionId = section.Id,
                            SectionName = section.Name,
                            Questions =

                            section.Questions.Select(q => new ParticipantSurveyQuestionViewModel()
                            {
                                IsMandatory = q.IsMandatory,
                                AnswerOptions = q.QuestionOptions
                                .ToList().Concat(q.QuestionType == QuestionType.AgreeDisagree.ToString() ?
                                                                            // Add records that don't exist in the database
                                                                            new List<string> { "Strongly Agree", "Agree", "Neutral", "Disagree", "Strongly Disagree" }
                                                                            .Select((x, i) => new QuestionOption()
                                                                            {
                                                                                IsOther = false,
                                                                                Text = x,
                                                                                Order = i,

                                                                            }) : Enumerable.Empty<QuestionOption>())
                                                .Select(o => new AnswerOptionViewModel()
                                                {
                                                    IsOther = o.IsOther,
                                                    OptionText = o.Text,
                                                    QuestionOptionId = o.Id
                                                }).ToList(),
                                QuestionId = q.Id,
                                QuestionText = q.Text,
                                QuestionType = (QuestionType)Enum.Parse(typeof(QuestionType), q.QuestionType),
                                Order = q.Order

                            }).ToList()

                        }).ToList()
                    }).FirstOrDefault();
                    







            }

            return Page();
        }
    }
}
