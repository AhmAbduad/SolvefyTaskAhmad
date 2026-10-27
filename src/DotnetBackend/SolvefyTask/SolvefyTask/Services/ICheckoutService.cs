using SolvefyTask.DTOs;
using SolvefyTask.Models;

namespace SolvefyTask.Services
{
    public interface ICheckoutService
    {
        Task<Checkout> CreateAsync(Checkout checkout);

        Task<List<EquipmentOptionDto>> GetEquipmentOptionsAsync();

        Task<string?> GetAssignedToByEquipmentIdAsync(int equipmentId);
    }
}
