using SolvefyTask.DTOs;

namespace SolvefyTask.Services
{
    public interface IApprovalService
    {
        Task<List<ApprovalResponseDto>> GetAllAsync();

        Task<ApprovalResponseDto?> GetByIdAsync(int id);

        Task<ApprovalResponseDto> CreateAsync(
            CreateApprovalRequestDto dto);

        Task<ApprovalResponseDto?> DecideAsync(
            int id,
            string decision,
            string? comments);
    }
}
