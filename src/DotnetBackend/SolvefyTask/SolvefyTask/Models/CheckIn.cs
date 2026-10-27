using System.ComponentModel.DataAnnotations;

namespace SolvefyTask.Models
{
    public class CheckIn
    {
        public int Id { get; set; }

        [Required]
        public int EquipmentId { get; set; }

        public DateTime ReturnDate { get; set; }

        public TimeSpan ReturnTime { get; set; }

        [Required]
        [MaxLength(30)]
        public string Condition { get; set; } = "Good";

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Equipment? Equipment { get; set; }
    }
}
