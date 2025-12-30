using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoMvc069.Models
{
    public class DaiLy
    {
        [Key]
        public string MaDaiLy { get; set; } = string.Empty;

        [Required]
        public string TenDaiLy { get; set; } = string.Empty;

        public string DiaChi { get; set; } = string.Empty;

        public string NguoiDaiDien { get; set; } = string.Empty;

        public string DienThoai { get; set; } = string.Empty;

        // Khóa ngoại
        public string MaHTPP { get; set; } = string.Empty;

        // Navigation property
        [ForeignKey("MaHTPP")]
        public HeThongPhanPhoi? HeThongPhanPhoi { get; set; }
    }
}
