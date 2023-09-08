using Microsoft.EntityFrameworkCore;

namespace DangKyPhongThucHanhCNTT.Models
{
    [PrimaryKey(nameof(HardwareId),nameof(SoftwareId))]
    public class InstallSoftware
    {
        public int HardwareId { get; set; }
        public Hardware? Hardware { get; set; }
        public int SoftwareId { get; set; }
        public Software? Software { get; set; }
    }
}
