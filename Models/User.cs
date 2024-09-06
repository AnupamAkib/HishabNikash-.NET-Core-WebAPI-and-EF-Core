using System.ComponentModel.DataAnnotations;

namespace HishabNikash.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? HashedPassword { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
