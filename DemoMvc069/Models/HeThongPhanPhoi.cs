public class HeThongPhanPhoi
{
    public string MaHTPP { get; set; }
    public string TenHTPP { get; set; }
    
    // Navigation property
    public virtual ICollection<DaiLy> DaiLies { get; set; }
}