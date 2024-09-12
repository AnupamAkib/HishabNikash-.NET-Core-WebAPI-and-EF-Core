using System.ComponentModel.DataAnnotations;

namespace HishabNikash.DTOs.RequestsDTO
{
    public class EditHishabRequestDTO
    {
        [Required]
        public int HishabID { get; set; }
        [Required]
        public string UpdatedHishabName { get; set; } = string.Empty;
        [Required]
        public string UpdatedCardColor { get; set; } = string.Empty;
    }
}
