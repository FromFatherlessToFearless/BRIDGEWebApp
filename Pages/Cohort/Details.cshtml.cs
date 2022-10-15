using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BRIDGEWebApp.Data;
using BRIDGEWebApp.Data.Models;

namespace BRIDGEWebApp.Pages.Cohort
{
    public class DetailsModel : PageModel
    {
        private readonly BRIDGEWebApp.Data.ApplicationDbContext _context;

        public DetailsModel(BRIDGEWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Data.Models.Cohort Cohort { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Cohorts == null)
            {
                return NotFound();
            }

            var cohort = await _context.Cohorts.FirstOrDefaultAsync(m => m.Id == id);
            if (cohort == null)
            {
                return NotFound();
            }
            else 
            {
                Cohort = cohort;
            }
            return Page();
        }
    }
}
