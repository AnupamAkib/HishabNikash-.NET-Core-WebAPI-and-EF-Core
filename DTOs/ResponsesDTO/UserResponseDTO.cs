using System.ComponentModel.DataAnnotations;

namespace HishabNikash.Payloads.Responses
{
    public class UserResponseDTO
    {
        public int UserID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? CreatedDate { get; set; }
    }
}
