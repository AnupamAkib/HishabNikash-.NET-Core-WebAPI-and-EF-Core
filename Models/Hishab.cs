using System.ComponentModel.DataAnnotations;

namespace HishabNikash.Models
{
    public class Hishab
    {
        [Key]
        public int HishabID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public string? Name { get; set; }
        public int Amount { get; set; } = 0;
        public string? CardColor { get; set; }
        public ICollection<History>? Histories { get; set; } = new List<History>();
    }
}
