using System.ComponentModel.DataAnnotations.Schema;

namespace DangKyPhongThucHanhCNTT.Models
{
    public class RAM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public ICollection<Hardware> Hardwares { get; } = new List<Hardware>();
    }
}
