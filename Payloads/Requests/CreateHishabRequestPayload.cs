using System.ComponentModel.DataAnnotations;

namespace HishabNikash.Payloads.Requests
{
    public class CreateHishabRequestPayload
    {
        [Required]
        public int UserID { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Hishab name is too long")]
        public string? Name { get; set; }
        public int Amount { get; set; } = 0;
        public string? CardColor { get; set; } = "Gray";
    }
}
