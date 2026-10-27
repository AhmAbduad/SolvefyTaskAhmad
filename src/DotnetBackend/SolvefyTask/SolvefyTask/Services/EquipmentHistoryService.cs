using SolvefyTask.DTOs;
using SolvefyTask.Repositories;

namespace SolvefyTask.Services
{
    public class EquipmentHistoryService
          : IEquipmentHistoryService
    {
        private readonly IEquipmentHistoryRepository _repository;

        public EquipmentHistoryService(
            IEquipmentHistoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<EquipmentHistoryDto>> GetAllAsync(
            DateTime? startDate,
            DateTime? endDate)
        {
            if (startDate.HasValue &&
                endDate.HasValue &&
                startDate.Value.Date > endDate.Value.Date)
            {
                throw new ArgumentException(
                    "Start date cannot be later than end date.");
            }

            var records = await _repository.GetAllAsync(
                startDate, endDate);

            return records.Select(x => new EquipmentHistoryDto
            {
                Id = x.Id,
                Date = x.Date,
                Equipment = x.Equipment,
                Action = x.Action,
                Person = x.Person,
                Remarks = x.Remarks
            }).ToList();
        }

        public async Task<EquipmentHistoryDto?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "A valid history record ID is required.");
            }

            var record = await _repository.GetByIdAsync(id);

            if (record == null)
            {
                return null;
            }

            return new EquipmentHistoryDto
            {
                Id = record.Id,
                Date = record.Date,
                Equipment = record.Equipment,
                Action = record.Action,
                Person = record.Person,
                Remarks = record.Remarks
            };
        }



        public async Task RecordActivityAsync(
            string equipment,
            string action,
            string person,
            string? remarks)
                {
                    if (string.IsNullOrWhiteSpace(equipment))
                        throw new ArgumentException("Equipment name is required.");

                    if (action != "Checked Out" && action != "Returned")
                        throw new ArgumentException("Invalid equipment history action.");

                    if (string.IsNullOrWhiteSpace(person))
                        throw new ArgumentException("Person is required.");

                    var history = new SolvefyTask.Models.EquipmentHistory
                    {
                        Date = DateTime.UtcNow,
                        Equipment = equipment.Trim(),
                        Action = action,
                        Person = person.Trim(),
                        Remarks = remarks?.Trim() ?? string.Empty
                    };

                    await _repository.CreateAsync(history);
                }
    }
}
