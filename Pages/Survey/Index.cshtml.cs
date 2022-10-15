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

namespace BRIDGEWebApp.Pages.Survey
{
    public class IndexModel : PageModel
    {
        private readonly BRIDGEWebApp.Data.ApplicationDbContext _context;

        public IndexModel(BRIDGEWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public IList<CohortViewModel> Cohorts { get; set; } = default!;
        public IList<SurveyViewModel> SurveyViewModels { get;set; } = default!;
 
        public async Task OnGetAsync()
        {
            if (_context.Surveys != null)
            {
                SurveyViewModels = await _context.Surveys
                .Include(s => s.CreatedByIdentityUser)
                .Include(s => s.UpdatedByIdentityUser)
                .Select(s => new SurveyViewModel
                {
                    Id = s.Id,
                    IsActive =s.IsActive,
                    Name = s.Name,
                    CreatedOn = s.CreatedOn,
                    UpdatedOn = s.UpdatedOn,
                }).ToListAsync();
            }
        }
    }
}
