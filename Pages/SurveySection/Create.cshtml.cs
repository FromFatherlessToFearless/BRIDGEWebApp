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
using Microsoft.AspNetCore.Authorization;

namespace BRIDGEWebApp.Pages.SurveySection
{
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class CreateModel : PageModel
    {
        private readonly BRIDGEWebApp.Data.ApplicationDbContext _context;

        public CreateModel(BRIDGEWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public SurveySectionViewModel SurveySection { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Data.Models.SurveySection surveySection = new Data.Models.SurveySection();
            surveySection.UpdatedBy = userId;
            surveySection.CreatedBy = userId;
            surveySection.UpdatedOn = DateTime.Now;
            surveySection.CreatedOn = DateTime.Now;
            surveySection.Description = SurveySection.Description;
            surveySection.IsActive = true;
            surveySection.Order = SurveySection.Order;
            surveySection.Name = SurveySection.Name;
            ModelState.Remove("SurveySection.CreatedBy");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.SurveySections.Add(surveySection);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
