namespace SolvefyTask.DTOs
{
    public class EquipmentHistoryDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Equipment { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string Person { get; set; } = string.Empty;
        public string Remarks { get; set; } = string.Empty;
    }
}
