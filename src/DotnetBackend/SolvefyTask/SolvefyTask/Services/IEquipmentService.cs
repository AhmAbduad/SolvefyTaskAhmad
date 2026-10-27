using SolvefyTask.Models;

namespace SolvefyTask.Services
{
    public interface IEquipmentService
    {
        Task<List<Equipment>> GetAllAsync();

        Task<Equipment> InsertAsync(Equipment equipment);
    }
}
