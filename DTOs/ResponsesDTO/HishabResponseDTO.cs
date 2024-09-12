using HishabNikash.Models;

namespace HishabNikash.Payloads.Responses
{
    public class HishabResponseDTO
    {
        public int HishabID { get; set; }
        public int UserID { get; set; }
        public string? Name { get; set; }
        public int Amount { get; set; } = 0;
        public string? CardColor { get; set; }
        //may add created date / last update
        public List<HistoryResponseDTO>? TransactionHistories { get; set; }
    }
}
