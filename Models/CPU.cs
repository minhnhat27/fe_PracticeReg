namespace DangKyPhongThucHanhCNTT.Models
{
    public class CPU
    {
        public string? Id { get; set; }
        public ICollection<Hardware> Hardwares { get; } = new List<Hardware>();
    }
}
