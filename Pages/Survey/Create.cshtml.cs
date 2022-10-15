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
using System.Drawing;

namespace BRIDGEWebApp.Pages.Survey
{
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
        public SurveyViewModel Survey { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Data.Models.Survey survey = new Data.Models.Survey();
            survey.UpdatedBy = userId;
            survey.CreatedBy = userId;
            survey.UpdatedOn = DateTime.Now;
            survey.CreatedOn = DateTime.Now;
            survey.Name = Survey.Name;
            survey.IsActive = true;
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Surveys.Add(survey);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
