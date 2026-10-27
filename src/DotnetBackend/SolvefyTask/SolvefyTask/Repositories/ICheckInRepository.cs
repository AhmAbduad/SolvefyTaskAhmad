using SolvefyTask.Models;

namespace SolvefyTask.Repositories
{
    public interface ICheckInRepository
    {
        Task<List<Equipment>> GetEquipmentAsync();

        Task<Equipment?> GetEquipmentByIdAsync(int equipmentId);

        Task<CheckIn> CreateCheckInAsync(CheckIn checkIn);
    }
}
