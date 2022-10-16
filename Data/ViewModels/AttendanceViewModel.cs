using System;
using BRIDGEWebApp.Data.Models;

namespace BRIDGEWebApp.Data.ViewModels
{
    public class AttendanceViewModel
    {
        public Participant SelectedParticipant { get; set; }

        public List<Participant> Participants { get; set; }

        public Cohort SelectedCohort { get; set; }

        public List<Cohort> Cohorts { get; set; }

        public DateTime Date { get; set; }

        public bool InAttendance { get; set; }
    }
}

