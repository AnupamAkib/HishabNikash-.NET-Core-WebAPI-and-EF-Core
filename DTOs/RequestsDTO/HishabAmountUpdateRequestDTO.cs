using System.ComponentModel.DataAnnotations;

namespace HishabNikash.Payloads.Requests
{
    public class HishabAmountUpdateRequestDTO
    {
        [Required]
        public int HishabID { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
