using SolvefyTask.DTOs;
using SolvefyTask.Models;
using SolvefyTask.Repositories;

namespace SolvefyTask.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly ICheckoutRepository _repository;

        public CheckoutService(ICheckoutRepository repository)
        {
            _repository = repository;
        }

        public async Task<Checkout> CreateAsync(Checkout checkout)
        {
            checkout.Id = 0;
            checkout.Status = "Pending";
            checkout.CreatedAt = DateTime.UtcNow;

            return await _repository.InsertAsync(checkout);
        }


        public Task<List<EquipmentOptionDto>> GetEquipmentOptionsAsync()
        {
            return _repository.GetEquipmentOptionsAsync();
        }

        public Task<string?> GetAssignedToByEquipmentIdAsync(int equipmentId)
        {
            return _repository.GetAssignedToByEquipmentIdAsync(equipmentId);
        }
    }
}
