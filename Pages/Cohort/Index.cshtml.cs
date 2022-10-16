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

namespace BRIDGEWebApp.Pages.Cohort
{
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class IndexModel : PageModel
    {
        private readonly BRIDGEWebApp.Data.ApplicationDbContext _context;

        public IndexModel(BRIDGEWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<CohortViewModel> Cohorts { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Cohorts != null)
            {
                Cohorts = await _context.Cohorts
                .Include(c => c.CreatedByIdentityUser)
                .Include(c => c.UpdatedByIdentityUser)
                .Select(c => new CohortViewModel
                {
                    Id = c.Id,
                    IsActive = c.IsActive,
                    Name = c.Name,
                    Description = c.Description,
                    IsRegistrationOpen = c.IsRegistrationOpen,
                    CreatedOn = c.CreatedOn,
                    StartDate = c.StartDate,
                    UpdatedOn = c.UpdatedOn,
                }).ToListAsync();
            }
        }
    }
}
