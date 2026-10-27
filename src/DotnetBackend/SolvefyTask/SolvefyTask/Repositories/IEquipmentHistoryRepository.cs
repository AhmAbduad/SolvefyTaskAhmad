using SolvefyTask.Models;

namespace SolvefyTask.Repositories
{
    public interface IEquipmentHistoryRepository
    {
        Task<List<EquipmentHistory>> GetAllAsync(
            DateTime? startDate,
            DateTime? endDate);

        Task<EquipmentHistory?> GetByIdAsync(int id);

        Task<EquipmentHistory> CreateAsync(
            EquipmentHistory history);
    }
}
