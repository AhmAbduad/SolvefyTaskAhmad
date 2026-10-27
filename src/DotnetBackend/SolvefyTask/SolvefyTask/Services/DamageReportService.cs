using SolvefyTask.DTOs;
using SolvefyTask.Models;
using SolvefyTask.Repositories;

namespace SolvefyTask.Services
{
    public class DamageReportService : IDamageReportService
    {
        private readonly IDamageReportRepository _repository;
        private readonly IEquipmentHistoryService _historyService;
        public DamageReportService(IDamageReportRepository repository, IEquipmentHistoryService historyService)
        {
            _repository = repository;
            _historyService = historyService;
        }

        public async Task<List<DamageReportDto>> GetAllAsync(
            string? search,
            string? status,
            DateTime? startDate,
            DateTime? endDate)
        {
            if (startDate.HasValue &&
                endDate.HasValue &&
                startDate.Value.Date > endDate.Value.Date)
            {
                throw new ArgumentException(
                    "Start date cannot be after end date.");
            }

            var reports = await _repository.GetAllAsync(
                search, status, startDate, endDate);

            return reports.Select(MapToDto).ToList();
        }

        public async Task<DamageReportDto?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "A valid damage report ID is required.");
            }

            var report = await _repository.GetByIdAsync(id);

            return report == null ? null : MapToDto(report);
        }

        public async Task<DamageReportDto> CreateAsync(
            CreateDamageReportDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Equipment))
            {
                throw new ArgumentException(
                    "Equipment name is required.");
            }

            if (string.IsNullOrWhiteSpace(request.Description))
            {
                throw new ArgumentException(
                    "Damage description is required.");
            }

            if (string.IsNullOrWhiteSpace(request.ReportedBy))
            {
                throw new ArgumentException(
                    "Reported by is required.");
            }

            var report = new DamageReport
            {
                Equipment = request.Equipment.Trim(),
                Serial = string.IsNullOrWhiteSpace(request.Serial)
                    ? "N/A"
                    : request.Serial.Trim(),
                Description = request.Description.Trim(),
                ReportedBy = request.ReportedBy.Trim(),
                Date = DateTime.UtcNow,
                Status = "Pending"
            };

            var created = await _repository.CreateAsync(report);
            await _historyService.RecordActivityAsync(
        equipment: created.Equipment,
        action: "Damage Reported",
        person: created.ReportedBy,
        remarks: created.Description
    );
            return MapToDto(created);
        }

        public async Task<(bool Success, string Message)> ResolveAsync(
            int id)
        {
            if (id <= 0)
            {
                return (false, "A valid damage report ID is required.");
            }

            var report = await _repository.GetByIdAsync(id);

            if (report == null)
            {
                return (false, "Damage report not found.");
            }

            if (report.Status == "Resolved")
            {
                return (false, "This damage report is already resolved.");
            }

            var resolved = await _repository.ResolveAsync(id);
            await _historyService.RecordActivityAsync(
        equipment: report.Equipment,
        action: "Damage Resolved",
        person: report.ReportedBy,
        remarks: report.Description
    );

            return resolved
                ? (true, "Damage report resolved successfully.")
                : (false, "Unable to resolve the damage report.");
        }

        private static DamageReportDto MapToDto(DamageReport report)
        {
            return new DamageReportDto
            {
                Id = report.Id,
                Date = report.Date,
                Equipment = report.Equipment,
                Serial = report.Serial,
                Description = report.Description,
                ReportedBy = report.ReportedBy,
                Status = report.Status
            };
        }
    }
}
