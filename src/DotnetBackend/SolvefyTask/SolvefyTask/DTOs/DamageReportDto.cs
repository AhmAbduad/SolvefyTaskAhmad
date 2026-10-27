namespace SolvefyTask.DTOs
{
    public class DamageReportDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Equipment { get; set; } = string.Empty;
        public string Serial { get; set; } = "N/A";
        public string Description { get; set; } = string.Empty;
        public string ReportedBy { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
    }

    public class CreateDamageReportDto
    {
        public string Equipment { get; set; } = string.Empty;
        public string? Serial { get; set; }
        public string Description { get; set; } = string.Empty;
        public string ReportedBy { get; set; } = string.Empty;
    }
}
