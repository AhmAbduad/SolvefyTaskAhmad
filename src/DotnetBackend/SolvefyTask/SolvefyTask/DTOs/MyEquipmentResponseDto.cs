namespace SolvefyTask.DTOs
{
    public class MyEquipmentResponseDto
    {
        public int Id { get; set; }
        public string EquipmentName { get; set; } = string.Empty;
        public string? SerialNumber { get; set; }
        public string? Type { get; set; }
        public DateTime CheckedOutDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
