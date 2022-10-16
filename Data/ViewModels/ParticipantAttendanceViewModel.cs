using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BRIDGEWebApp.Data.ViewModels
{
    public class ParticipantAttendanceViewModel
    {
        public int? ParticipantAttendanceId { get; set; }

        public int ParticipantId { get; set; }
        [DisplayName("In Attendance")]
        public bool InAttendance { get; set; }
        [StringLength(100)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [StringLength(100)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
    }
}
