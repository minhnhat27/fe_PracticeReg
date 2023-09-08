using System.ComponentModel.DataAnnotations.Schema;

namespace DangKyPhongThucHanhCNTT.Models
{
    public class CourseGroup
    {
        public string? Id { get; set; }
        public int NumberOfStudents { get; set; }
        public Course? Course { get; set; }
    }
}
