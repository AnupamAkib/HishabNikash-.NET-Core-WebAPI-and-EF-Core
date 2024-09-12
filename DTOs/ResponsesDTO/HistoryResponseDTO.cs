using System.ComponentModel.DataAnnotations;

namespace HishabNikash.Payloads.Responses
{
    public class HistoryResponseDTO
    {
        public string? HistoryName { get; set; }
        public string? HistoryType { get; set; }
        public string? CreatedDate { get; set; }
    }
}
