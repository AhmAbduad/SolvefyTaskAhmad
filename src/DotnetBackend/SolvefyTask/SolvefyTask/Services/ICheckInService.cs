using SolvefyTask.Models;

namespace SolvefyTask.Services
{
    public interface ICheckInService
    {
        Task<List<Equipment>> GetEquipmentAsync();

        Task<Equipment?> GetEquipmentByIdAsync(int equipmentId);

        Task<CheckIn> CreateCheckInAsync(CheckIn checkIn);
    }
}
