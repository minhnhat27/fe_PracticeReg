using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DangKyPhongThucHanhCNTT.Models
{
    [PrimaryKey(nameof(CourseGroupId), nameof(NumberOfSessionId), nameof(LecturerId), nameof(SemesterId))]
    public class Teaching
    {
        public string? CourseGroupId { get; set; }
        public CourseGroup? CourseGroup { get; set; }
        public int NumberOfSessionId { get; set; }
        public NumberOfSession? NumberOfSession { get; set; }
        public string? LecturerId { get; set; }
        public Lecturer? Lecturer { get; set; }
        public string? SemesterId { get; set; }
        public Semester? Semester { get; set; }
        public ICollection<Schedule> Schedules { get; } = new List<Schedule>();
    }
}
