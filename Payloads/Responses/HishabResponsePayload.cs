using HishabNikash.Models;

namespace HishabNikash.Payloads.Responses
{
    public class HishabResponsePayload
    {
        public int HishabID { get; set; }
        public int UserID { get; set; }
        public string? Name { get; set; }
        public int Amount { get; set; } = 0;
        public string? CardColor { get; set; }
        //may add created date / last update
        public List<HistoryResponsePayload>? TransactionHistories { get; set; }
    }
}
