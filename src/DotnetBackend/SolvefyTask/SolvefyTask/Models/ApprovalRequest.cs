using System.ComponentModel.DataAnnotations;

namespace SolvefyTask.Models
{
    public class ApprovalRequest
    {
        public int Id { get; set; }

        [Required]
        public int EquipmentId { get; set; }

        [Required]
        [MaxLength(100)]
        public string RequestedBy { get; set; } = string.Empty;

        public DateTime RequestedOn { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime ExpectedReturn { get; set; }

        [Required]
        [MaxLength(500)]
        public string Purpose { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string Status { get; set; } = "Pending Approval";

        [MaxLength(1000)]
        public string? Comments { get; set; }

        public DateTime? DecisionAt { get; set; }

        public Equipment? Equipment { get; set; }
    }
}
