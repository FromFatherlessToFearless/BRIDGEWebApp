using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BRIDGEWebApp.Data;
using BRIDGEWebApp.Data.Models;

namespace BRIDGEWebApp.Pages.SurveySection
{
    public class DetailsModel : PageModel
    {
        private readonly BRIDGEWebApp.Data.ApplicationDbContext _context;

        public DetailsModel(BRIDGEWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Data.Models.SurveySection SurveySection { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.SurveySections == null)
            {
                return NotFound();
            }

            var surveysection = await _context.SurveySections.FirstOrDefaultAsync(m => m.Id == id);
            if (surveysection == null)
            {
                return NotFound();
            }
            else 
            {
                SurveySection = surveysection;
            }
            return Page();
        }
    }
}
