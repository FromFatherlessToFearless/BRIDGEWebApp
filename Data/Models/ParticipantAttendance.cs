using System.ComponentModel.DataAnnotations;

namespace BRIDGEWebApp.Data.Models
{
    public class ParticipantAttendance
    {
        [Key]
        public int ParticipantAttendanceId { get; set; }

        public int ParticipantId { get; set; }

        public int AttendanceId { get; set; }

        public bool InAttendance { get; set; }

        public Participant Participant { get; set; }

        public Attendance Attendance { get; set; }
    }
}

