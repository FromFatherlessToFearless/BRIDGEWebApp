using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BRIDGEWebApp.Data;
using BRIDGEWebApp.Data.Models;

namespace BRIDGEWebApp.Pages.Attendance
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
        ViewData["Cohorts"] = new SelectList(_context.Cohorts, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public BRIDGEWebApp.Data.Models.Attendance Attendance { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Attendances == null || Attendance == null)
            {
                return Page();
            }

            _context.Attendances.Add(Attendance);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
