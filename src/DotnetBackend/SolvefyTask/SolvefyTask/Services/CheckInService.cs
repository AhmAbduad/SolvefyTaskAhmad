using SolvefyTask.Models;
using SolvefyTask.Repositories;

namespace SolvefyTask.Services
{
    public class CheckInService : ICheckInService
    {
        private readonly ICheckInRepository _repository;

        public CheckInService(ICheckInRepository repository)
        {
            _repository = repository;
        }

        public Task<List<Equipment>> GetEquipmentAsync()
        {
            return _repository.GetEquipmentAsync();
        }

        public Task<Equipment?> GetEquipmentByIdAsync(int equipmentId)
        {
            return _repository.GetEquipmentByIdAsync(equipmentId);
        }

        public async Task<CheckIn> CreateCheckInAsync(CheckIn checkIn)
        {
            checkIn.Id = 0;

            return await _repository.CreateCheckInAsync(checkIn);
        }
    }
}
