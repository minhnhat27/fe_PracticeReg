using DangKyPhongThucHanhCNTT.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DangKyPhongThucHanhCNTT.Models
{
    public class NumberOfSession
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
    }
}