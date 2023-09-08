using Microsoft.EntityFrameworkCore;

namespace DangKyPhongThucHanhCNTT.Models
{
    [PrimaryKey(nameof(RoomId),nameof(CourseId))]
    public class SuitableCourse
    {
        public int RoomId { get; set; }
        public Room? Room { get; set; }
        public string? CourseId { get; set; }
        public Course? Course { get; set; }
    }
}
