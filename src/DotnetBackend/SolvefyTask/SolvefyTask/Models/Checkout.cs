using System.ComponentModel.DataAnnotations;

namespace SolvefyTask.Models
{
    public class Checkout
    {
        public int Id { get; set; }

        [Required]
        public string EquipmentName { get; set; } = string.Empty;

        [Required]
        public string AssignedTo { get; set; } = string.Empty;

        public DateTime ExpectedReturnDate { get; set; }

        public string Purpose { get; set; } = string.Empty;

        public string Status { get; set; } = "Pending";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
