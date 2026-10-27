namespace SolvefyTask.Models
{
    public class DamageReport
    {
        public int Id { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public string Equipment { get; set; } = string.Empty;

        public string Serial { get; set; } = "N/A";

        public string Description { get; set; } = string.Empty;

        public string ReportedBy { get; set; } = string.Empty;

        public string Status { get; set; } = "Pending";
    }
}
