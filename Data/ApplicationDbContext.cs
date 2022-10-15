using BRIDGEWebApp.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BRIDGEWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Participant> Participants { get; set; }

        public DbSet<Cohort> Cohorts { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<QuestionOption> QuestionOptions { get; set; }  

        public DbSet<Survey> Surveys { get; set; }

        public DbSet<SurveySection> SurveySections { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<AnswerOption> AnswerOptions { get; set; }

        public DbSet<ParticipantSurvey> ParticipantSurveys { get; set; }
    }
}