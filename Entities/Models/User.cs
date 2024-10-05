using System.ComponentModel.DataAnnotations;

namespace HishabNikash.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? HashedPassword { get; set; }
        public string CreatedDate { get; set; } = DateTime.Now.ToString();

        //derived property
        public string FullName => $"{FirstName} {LastName}";
    }
}
