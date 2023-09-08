using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DangKyPhongThucHanhCNTT.Models
{
    public class Room
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int NumberOfComputers { get; set; }
        public Hardware? Hardware { get; set; }
    }
}
