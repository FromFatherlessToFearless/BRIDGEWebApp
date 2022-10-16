using System.ComponentModel.DataAnnotations;

namespace BRIDGEWebApp.Data.ViewModels
{
    public class ParticipantAttendanceListViewModel
    {
        public int AttendanceId { get; set; }
        public DateTime AttendanceDate { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public int CohortId { get; set; }
        public string CohortName { get; set; }
        public List<ParticipantAttendanceViewModel> ParticipantAttendance { get; set; }
        public ParticipantAttendanceListViewModel()
        {
            ParticipantAttendance = new List<ParticipantAttendanceViewModel>();
        }

    }
}
