using SolvefyTask.Models;

namespace SolvefyTask.Repositories
{
    public interface IDamageReportRepository
    {
        Task<List<DamageReport>> GetAllAsync(
            string? search,
            string? status,
            DateTime? startDate,
            DateTime? endDate);

        Task<DamageReport?> GetByIdAsync(int id);

        Task<DamageReport> CreateAsync(DamageReport report);

        Task<bool> ResolveAsync(int id);
    }
}
