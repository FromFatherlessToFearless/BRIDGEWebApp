using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BRIDGEWebApp.Data;
using BRIDGEWebApp.Data.Models;

namespace BRIDGEWebApp.Pages.Attendance
{
    public class IndexModel : PageModel
    {
        private readonly BRIDGEWebApp.Data.ApplicationDbContext _context;

        public IndexModel(BRIDGEWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<BRIDGEWebApp.Data.Models.Attendance> Attendance { get;set; } = default!;

        public async Task OnGetAsync(int cohortId)
        {

            if (_context.Attendances != null)
            {
                Attendance = await _context.Attendances
                .Include(a => a.Cohort)
                .Include(a => a.CreatedByIdentityUser)
                .Include(a => a.UpdatedByIdentityUser).Where(a => a.CohortId == cohortId).ToListAsync();
            }
        }
    }
}
