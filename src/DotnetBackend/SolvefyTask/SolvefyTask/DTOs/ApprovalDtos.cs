using System.ComponentModel.DataAnnotations;

namespace SolvefyTask.DTOs
{
    public class CreateApprovalRequestDto
    {
        [Range(1, int.MaxValue)]
        public int EquipmentId { get; set; }

        [Required]
        [MaxLength(100)]
        public string RequestedBy { get; set; } = string.Empty;

        [Required]
        public DateTime ExpectedReturn { get; set; }

        [Required]
        [MaxLength(500)]
        public string Purpose { get; set; } = string.Empty;
    }

    public class ApprovalDecisionDto
    {
        [MaxLength(1000)]
        public string? Comments { get; set; }
    }

    public class ApprovalResponseDto
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; } = string.Empty;
        public string? SerialNumber { get; set; }
        public string RequestedBy { get; set; } = string.Empty;
        public DateTime RequestedOn { get; set; }
        public DateTime ExpectedReturn { get; set; }
        public string Purpose { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string? Comments { get; set; }
        public DateTime? DecisionAt { get; set; }
    }
}
