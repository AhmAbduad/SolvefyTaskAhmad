using SolvefyTask.Models;

namespace SolvefyTask.Repositories
{
    public interface IApprovalRepository
    {
        Task<List<ApprovalRequest>> GetAllAsync();

        Task<ApprovalRequest?> GetByIdAsync(int id);

        Task<bool> EquipmentExistsAsync(int equipmentId);

        Task<ApprovalRequest> CreateAsync(ApprovalRequest request);

        Task<ApprovalRequest?> DecideAsync(int id,string decision,string? comments);
    }
}
