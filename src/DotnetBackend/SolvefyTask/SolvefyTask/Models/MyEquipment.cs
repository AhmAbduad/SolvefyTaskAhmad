namespace SolvefyTask.Models
{
    public class MyEquipment
    {
        public int Id { get; set; }

        public int EquipmentId { get; set; }

        public string AssignedTo { get; set; } = string.Empty;

        public DateTime CheckedOutDate { get; set; }

        public DateTime ExpectedReturnDate { get; set; }

        public string Status { get; set; } = "Checked Out";

        // Navigation property
        public Equipment Equipment { get; set; } = null!;
    }
}
