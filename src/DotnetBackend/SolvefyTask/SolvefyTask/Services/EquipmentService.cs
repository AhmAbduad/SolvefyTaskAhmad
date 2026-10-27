using SolvefyTask.Models;
using SolvefyTask.Repositories;

namespace SolvefyTask.Services
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentRepository _repository;

        public EquipmentService(IEquipmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Equipment>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Equipment> InsertAsync(Equipment equipment)
        {
            equipment.Id = 0;
            equipment.Status = "Available";
            equipment.AssignedTo = "Ahmad";

            return await _repository.InsertAsync(equipment);
        }
    }
}
