using System.ComponentModel.DataAnnotations;

namespace DemoMvc069.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [Range(1, 100)]        
        public string Genre { get; set; }
        public decimal Price { get; set; }
    }
}