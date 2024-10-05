using System.ComponentModel.DataAnnotations;

namespace HishabNikash.Models
{
    public class History
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string? HistoryName { get; set; }
        [Required]
        public string? HistoryType { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int HishabID { get; set; }
        public Hishab? Hishab { get; set; }
    }
}
