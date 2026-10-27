using SolvefyTask.DTOs;
using SolvefyTask.Models;
using SolvefyTask.Repositories;

namespace SolvefyTask.Services
{
    public class ApprovalService : IApprovalService
    {
        private readonly IApprovalRepository _repository;
        private readonly IEquipmentHistoryService _historyService;
        public ApprovalService(IApprovalRepository repository, IEquipmentHistoryService historyService)
        {
            _repository = repository;
            _historyService = historyService;
        }

        public async Task<List<ApprovalResponseDto>> GetAllAsync()
        {
            var requests = await _repository.GetAllAsync();

            return requests.Select(MapToDto).ToList();
        }

        public async Task<ApprovalResponseDto?> GetByIdAsync(int id)
        {
            var request = await _repository.GetByIdAsync(id);

            return request == null ? null : MapToDto(request);
        }

        public async Task<ApprovalResponseDto> CreateAsync(
            CreateApprovalRequestDto dto)
        {
            if (!await _repository.EquipmentExistsAsync(dto.EquipmentId))
                throw new KeyNotFoundException(
                    "The selected equipment was not found.");

            if (dto.ExpectedReturn.Date < DateTime.UtcNow.Date)
                throw new ArgumentException(
                    "Expected return date cannot be in the past.");

            var request = new ApprovalRequest
            {
                EquipmentId = dto.EquipmentId,
                RequestedBy = dto.RequestedBy.Trim(),
                ExpectedReturn = dto.ExpectedReturn,
                Purpose = dto.Purpose.Trim(),
                Status = "Pending Approval"
            };

            var created = await _repository.CreateAsync(request);

            return MapToDto(created);
        }

        public async Task<ApprovalResponseDto?> DecideAsync(
            int id,
            string decision,
            string? comments)
        {
            var request = await _repository.DecideAsync(
                id, decision, comments);
            // Record history only when approval also completes checkout.
            if (request.Status == "Approved")
            {
                await _historyService.RecordActivityAsync(
                    equipment: request.Equipment?.Name
                        ?? throw new InvalidOperationException(
                            "Equipment information was not loaded."),
                    action: "Checked Out",
                    person: request.RequestedBy,
                    remarks: string.IsNullOrWhiteSpace(comments)
                        ? "Approved drone checkout"
                        : comments);
            }
            return request == null ? null : MapToDto(request);
        }

        private static ApprovalResponseDto MapToDto(
            ApprovalRequest request)
        {

            return new ApprovalResponseDto
            {
                Id = request.Id,
                EquipmentId = request.EquipmentId,
                EquipmentName = request.Equipment?.Name ?? string.Empty,
                SerialNumber = request.Equipment?.SerialNumber,
                RequestedBy = request.RequestedBy,
                RequestedOn = request.RequestedOn,
                ExpectedReturn = request.ExpectedReturn,
                Purpose = request.Purpose,
                Status = request.Status,
                Comments = request.Comments,
                DecisionAt = request.DecisionAt
            };
        }
    }
}
