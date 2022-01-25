using System.ComponentModel.DataAnnotations;

namespace API_Examen.DTO
{
    public class ActivityDTO
    {
        [Required]
        public int PropertyId { get; set; }
        [Required]
        public DateTime Schedule { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
