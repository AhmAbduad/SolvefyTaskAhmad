using SolvefyTask.DTOs;

namespace SolvefyTask.Services
{
    public interface IMyEquipmentService
    {
        Task<IEnumerable<MyEquipmentResponseDto>> GetMyEquipmentAsync(string assignedTo);
        Task<MyEquipmentResponseDto?> GetByIdAsync(int id);

        Task<(bool Success, string Message)> ReturnEquipmentAsync(int id);
    }
}
