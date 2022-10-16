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
using Microsoft.AspNetCore.Authorization;

namespace BRIDGEWebApp.Pages.SurveySection
{
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class IndexModel : PageModel
    {
        private readonly BRIDGEWebApp.Data.ApplicationDbContext _context;

        public IndexModel(BRIDGEWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<SurveySectionViewModel> SurveySection { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.SurveySections != null)
            {
                SurveySection = await _context.SurveySections
                .Include(s => s.CreatedByIdentityUser)
                .Include(s => s.UpdatedByIdentityUser).Select(s => new SurveySectionViewModel
                {
                    Id = s.Id,
                    IsActive = s.IsActive,
                    Name = s.Name,
                    Description = s.Description,
                    CreatedOn = s.CreatedOn,
                    UpdatedOn = s.UpdatedOn,
                    CreatedBy = s.CreatedBy,
                    UpdatedBy = s.UpdatedBy,
                    Order = s.Order 
                }).OrderBy(s => s.Order).ToListAsync();
            }
        }
    }
}
