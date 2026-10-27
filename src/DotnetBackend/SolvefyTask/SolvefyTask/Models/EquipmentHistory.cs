namespace SolvefyTask.Models
{
    public class EquipmentHistory
    {
        public int Id { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public string Equipment { get; set; } = string.Empty;

        // Allowed values: Checked Out, Returned
        public string Action { get; set; } = string.Empty;

        public string Person { get; set; } = string.Empty;

        public string Remarks { get; set; } = string.Empty;
    }
}
