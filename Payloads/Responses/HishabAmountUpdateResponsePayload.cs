namespace HishabNikash.Payloads.Responses
{
    public class HishabAmountUpdateResponsePayload
    {
        public int HishabID { get; set; }
        public int UserID { get; set; }
        public string? Name { get; set; }
        public int UpdatedAmount { get; set; }
    }
}
