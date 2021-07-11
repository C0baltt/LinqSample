using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class User
    {
        public bool IsNew => Id == 0;

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, 100)]
        public int? Age { get; set; }
    }
}
