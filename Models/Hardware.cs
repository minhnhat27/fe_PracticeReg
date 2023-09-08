namespace DangKyPhongThucHanhCNTT.Models
{
    public class Hardware
    {
        public int Id { get; set; }
        public CPU? CPU { get; set; }
        public RAM? RAM { get; set; }
        public HardDrive? HardDrive { get; set; }
        public ICollection<Room> Rooms { get; } = new List<Room>();
    }
}
