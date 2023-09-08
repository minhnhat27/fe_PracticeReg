using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace DangKyPhongThucHanhCNTT.Models
{
    public class Week
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public ICollection<Schedule> Schedules { get; } = new List<Schedule>();
    }
}
