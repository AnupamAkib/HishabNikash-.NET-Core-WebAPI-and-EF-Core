using System.ComponentModel.DataAnnotations;

namespace HishabNikash.Models
{
    public class Hishab
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public string? Name { get; set; }
        public int Amount { get; set; } = 0;
        public string? CardColor { get; set; }
        //may add created date / last update
        public ICollection<History>? Histories { get; set; } = new List<History>();
    }
}
