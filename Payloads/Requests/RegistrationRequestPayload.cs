using System.ComponentModel.DataAnnotations;

namespace HishabNikash.Payloads.Requests
{
    public class RegistrationRequestPayload
    {
        [Required]
        [MaxLength(20, ErrorMessage = "First name is too large")]
        public string? FirstName { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Last name is too large")]
        public string? LastName { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? HashedPassword { get; set; }

        //derived property
        public string FullName => $"{FirstName} {LastName}"; 
    }
}
