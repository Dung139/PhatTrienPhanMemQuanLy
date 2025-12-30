using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DemoMvc069.Models
{
    public class HeThongPhanPhoi
    {
        [Key]
        public string MaHTPP { get; set; } = string.Empty;

        [Required]
        public string TenHTPP { get; set; } = string.Empty;

    }
}
