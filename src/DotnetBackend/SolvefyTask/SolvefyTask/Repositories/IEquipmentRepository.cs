using SolvefyTask.Models;

namespace SolvefyTask.Repositories
{
    public interface IEquipmentRepository
    {
        Task<List<Equipment>> GetAllAsync();

        Task<Equipment> InsertAsync(Equipment equipment);
    }
}
