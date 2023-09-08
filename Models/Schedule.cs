using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DangKyPhongThucHanhCNTT.Models
{
    [PrimaryKey(nameof(Day),nameof(TimeId),nameof(RoomId))]
    public class Schedule
    {
        public string? Day { set; get; }
        public string? TimeId { get; set; }
        public Time? Time { get; set; }
        public int? RoomId { get; set; }
        public Room? Room { get; set; }

        public Week? Week { get; set; }
        public Teaching? Teaching { get; set; }
        public string? Note { get; set; }
    }
}
