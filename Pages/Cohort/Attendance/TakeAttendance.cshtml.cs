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
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BRIDGEWebApp.Pages.Attendance
{
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class CreateModel : PageModel
    {
        private readonly BRIDGEWebApp.Data.ApplicationDbContext _context;

        public CreateModel(BRIDGEWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ParticipantAttendanceListViewModel ParticipantAttendanceViewModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? cohortId)
        {
            if (cohortId == null || _context.Cohorts == null)
            {
                return NotFound();
            }
            ParticipantAttendanceViewModel = new ParticipantAttendanceListViewModel();
            var cohort = await _context.Cohorts.FirstOrDefaultAsync(m => m.Id == cohortId);
            if (cohort == null)
            {
                return NotFound();
            }

            if (_context.Participants != null)
            {
                var value = _context.Participants.Where(s => s.CohortId == cohortId);
                ParticipantAttendanceViewModel.ParticipantAttendance = await _context.Participants
                .Where(s => s.CohortId == cohortId)
                .Select(s => new ParticipantAttendanceViewModel
                {
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    InAttendance = false,
                    ParticipantId = s.Id,
                    ParticipantAttendanceId = 0
                })
                .ToListAsync();
                ParticipantAttendanceViewModel.CohortId = cohort.Id;
                ParticipantAttendanceViewModel.CohortName = cohort.Name;
                ParticipantAttendanceViewModel.AttendanceDate = System.DateTime.Now;
                ParticipantAttendanceViewModel.Name = ParticipantAttendanceViewModel.CohortName + " " + ParticipantAttendanceViewModel.AttendanceDate.ToShortDateString();
            }
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int cohortId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var attendance = new Data.Models.Attendance();
            attendance.AttendanceDate = ParticipantAttendanceViewModel.AttendanceDate;
            attendance.UpdatedBy = userId;
            attendance.CreatedBy = userId;
            attendance.UpdatedOn = DateTime.Now;
            attendance.CreatedOn = DateTime.Now;
            attendance.IsActive = true;
            attendance.CohortId = ParticipantAttendanceViewModel.CohortId;
            attendance.Name = ParticipantAttendanceViewModel.Name;
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Attendances.Add(attendance);
            await _context.SaveChangesAsync();

            foreach(var attendant in ParticipantAttendanceViewModel.ParticipantAttendance)
            {
                var participantAttendanceItem = new Data.Models.ParticipantAttendance();
                participantAttendanceItem.AttendanceId = attendance.AttendanceId;
                participantAttendanceItem.ParticipantId = attendant.ParticipantId;
                participantAttendanceItem.InAttendance = attendant.InAttendance;
                if (!ModelState.IsValid)
                {
                    return Page();
                }
                _context.ParticipantAttendances.Add(participantAttendanceItem);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("../Index");
        }
    }
}
