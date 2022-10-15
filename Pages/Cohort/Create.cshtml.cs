﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Security.Claims;

namespace BRIDGEWebApp.Pages.Cohort
{
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class CreateModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public CreateModel(BRIDGEWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            
            return Page();
        }

        [BindProperty]
        public Data.Models.Cohort Cohort { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Cohort.UpdatedBy = userId;
            Cohort.CreatedBy = userId;
            Cohort.UpdatedOn = DateTime.Now;
            Cohort.CreatedOn = DateTime.Now;
            ModelState.Clear();
            TryValidateModel(Cohort);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Cohorts.Add(Cohort);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
