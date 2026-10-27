using SolvefyTask.DTOs;

namespace SolvefyTask.Services
{
    public interface IDamageReportService
    {
        Task<List<DamageReportDto>> GetAllAsync(
            string? search,
            string? status,
            DateTime? startDate,
            DateTime? endDate);

        Task<DamageReportDto?> GetByIdAsync(int id);

        Task<DamageReportDto> CreateAsync(
            CreateDamageReportDto request);

        Task<(bool Success, string Message)> ResolveAsync(int id);
    }
}
