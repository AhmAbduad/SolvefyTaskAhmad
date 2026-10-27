using SolvefyTask.DTOs;
using SolvefyTask.Models;

namespace SolvefyTask.Repositories
{
    public interface ICheckoutRepository
    {
        Task<Checkout> InsertAsync(Checkout checkout);

        Task<List<EquipmentOptionDto>> GetEquipmentOptionsAsync();

        Task<string?> GetAssignedToByEquipmentIdAsync(int equipmentId);

    }
}
