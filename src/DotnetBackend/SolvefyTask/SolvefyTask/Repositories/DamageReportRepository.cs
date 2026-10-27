using Microsoft.EntityFrameworkCore;
using SolvefyTask.Data;
using SolvefyTask.Models;

namespace SolvefyTask.Repositories
{
    public class DamageReportRepository : IDamageReportRepository
    {
        private readonly ApplicationDbContext _context;

        public DamageReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DamageReport>> GetAllAsync(
            string? search,
            string? status,
            DateTime? startDate,
            DateTime? endDate)
        {
            IQueryable<DamageReport> query =
                _context.DamageReports.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.Trim().ToLower();

                query = query.Where(x =>
                    x.Equipment.ToLower().Contains(term) ||
                    x.Serial.ToLower().Contains(term) ||
                    x.Description.ToLower().Contains(term) ||
                    x.ReportedBy.ToLower().Contains(term));
            }

            if (!string.IsNullOrWhiteSpace(status) &&
                !status.Equals("All Status",
                    StringComparison.OrdinalIgnoreCase))
            {
                query = query.Where(x => x.Status == status);
            }

            if (startDate.HasValue)
            {
                var start = startDate.Value.Date;
                query = query.Where(x => x.Date >= start);
            }

            if (endDate.HasValue)
            {
                var endExclusive = endDate.Value.Date.AddDays(1);
                query = query.Where(x => x.Date < endExclusive);
            }

            return await query
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        public async Task<DamageReport?> GetByIdAsync(int id)
        {
            return await _context.DamageReports
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<DamageReport> CreateAsync(DamageReport report)
        {
            _context.DamageReports.Add(report);
            await _context.SaveChangesAsync();

            return report;
        }

        public async Task<bool> ResolveAsync(int id)
        {
            var report = await _context.DamageReports
                .FirstOrDefaultAsync(x => x.Id == id);

            if (report == null)
            {
                return false;
            }

            report.Status = "Resolved";

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
