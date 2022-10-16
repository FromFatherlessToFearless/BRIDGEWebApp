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

namespace BRIDGEWebApp.Pages.Survey
{
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class DetailsModel : PageModel
    {
        private readonly BRIDGEWebApp.Data.ApplicationDbContext _context;

        public DetailsModel(BRIDGEWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Data.Models.Survey Survey { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Surveys == null)
            {
                return NotFound();
            }

            var survey = await _context.Surveys.FirstOrDefaultAsync(m => m.Id == id);
            if (survey == null)
            {
                return NotFound();
            }
            else 
            {
                Survey = survey;
            }
            return Page();
        }
    }
}
