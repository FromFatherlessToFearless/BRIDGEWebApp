using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BRIDGEWebApp.Data.Models
{
    public class Attendance
    {
        [Key]
        public int AttendanceId { get; set; }

        public DateTime AttendanceDate { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public int CohortId { get; set; }

        public Cohort Cohort { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string CreatedBy { get; set; }

        [ForeignKey("CreatedBy")]
        public IdentityUser CreatedByIdentityUser { get; set; }

        public string UpdatedBy { get; set; }

        [ForeignKey("UpdatedBy")]
        public IdentityUser UpdatedByIdentityUser { get; set; }

    }
}

