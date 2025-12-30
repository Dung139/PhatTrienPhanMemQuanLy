using System.ComponentModel.DataAnnotations;

namespace DemoMvc069.Models
{
    public class Employee : Person
    {
        [Required]
        public string EmployeeId { get; set; }

        public int Age { get; set; }
    }
}
