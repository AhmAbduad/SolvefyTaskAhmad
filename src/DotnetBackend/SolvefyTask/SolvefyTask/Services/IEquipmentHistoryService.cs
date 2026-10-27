using SolvefyTask.DTOs;

namespace SolvefyTask.Services
{
    public interface IEquipmentHistoryService
    {
        Task<List<EquipmentHistoryDto>> GetAllAsync(
            DateTime? startDate,
            DateTime? endDate);

        Task<EquipmentHistoryDto?> GetByIdAsync(int id);

        Task RecordActivityAsync(
            string equipment,
            string action,
            string person,
            string? remarks);
    }
}
