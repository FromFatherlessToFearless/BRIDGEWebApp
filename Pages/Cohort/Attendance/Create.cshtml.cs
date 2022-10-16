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
            ViewData["Participants"] = new SelectList(_context.Participants, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public AttendanceViewModel AttendanceViewModel { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Attendances == null || AttendanceViewModel == null)
            {
                return Page();
            }

            var attendance = new BRIDGEWebApp.Data.Models.Attendance
            {
                AttendanceDate = AttendanceViewModel.Date,
                Cohort = AttendanceViewModel.SelectedCohort,
                CreatedOn = DateTime.Today
            };

            var participantAttendance = new ParticipantAttendance
            {
                Attendance = attendance,
                InAttendance = AttendanceViewModel.InAttendance,
                AttendanceId = attendance.AttendanceId,
                Participant = AttendanceViewModel.SelectedParticipant,
                ParticipantId = AttendanceViewModel.SelectedParticipant.Id
            };

            _context.Attendances.Add(attendance);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
