using SolvefyTask.DTOs;
using SolvefyTask.Repositories;

namespace SolvefyTask.Services
{
    public class MyEquipmentService : IMyEquipmentService
    {
        private readonly IMyEquipmentRepository _repository;
        private readonly IEquipmentHistoryService _historyService;
        public MyEquipmentService(
            IMyEquipmentRepository repository,
            IEquipmentHistoryService historyService)
        {
            _repository = repository;
            _historyService = historyService;
        }

        public async Task<IEnumerable<MyEquipmentResponseDto>>
            GetMyEquipmentAsync(string assignedTo)
        {
            if (string.IsNullOrWhiteSpace(assignedTo))
            {
                throw new ArgumentException(
                    "Assigned employee is required.");
            }

            return await _repository.GetMyEquipmentAsync(
                assignedTo.Trim());
        }

        public async Task<MyEquipmentResponseDto?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "A valid equipment checkout ID is required.");
            }

            return await _repository.GetByIdAsync(id);
        }

        public async Task<(bool Success, string Message)>
            ReturnEquipmentAsync(int id)
        {
            if (id <= 0)
            {
                return (false, "A valid checkout ID is required.");
            }

            var equipment = await _repository.GetByIdAsync(id);

            if (equipment == null)
            {
                return (false, "Equipment checkout record not found.");
            }

            if (equipment.Status == "Returned")
            {
                return (false, "This equipment has already been returned.");
            }

            var returned = await _repository.ReturnEquipmentAsync(id);

            if (!returned)
            {
                return (false, "Unable to return the equipment.");
            }
            await _historyService.RecordActivityAsync(
                equipment: equipment.EquipmentName,
                action: "Returned",
                person: "Ahmad",
                remarks: "Equipment returned"
            );
            return (true, "Equipment returned successfully.");
        }
    }
}
