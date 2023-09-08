using System.ComponentModel.DataAnnotations;

namespace DangKyPhongThucHanhCNTT.Models
{
    public class Course
    {
        public string? Id { get; set; }
        public string? CourseName { get; set; }
        public int CreditHours { get; set; }
        public int NumberOfPractice { get; set; }

        public ICollection<CourseGroup>? CourseGroup { get; } = new List<CourseGroup>();
    }
}
