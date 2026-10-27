using SolvefyTask.DTOs;

namespace SolvefyTask.Repositories
{
    public interface IMyEquipmentRepository
    {
        Task<IEnumerable<MyEquipmentResponseDto>> GetMyEquipmentAsync(string assignedTo);

        Task<MyEquipmentResponseDto?> GetByIdAsync(int id);

        Task<bool> ReturnEquipmentAsync(int id);
    }
}
