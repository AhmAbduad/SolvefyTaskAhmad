using Microsoft.EntityFrameworkCore;
using SolvefyTask.Data;
using SolvefyTask.Models;

namespace SolvefyTask.Repositories
{
    public class EquipmentHistoryRepository
        : IEquipmentHistoryRepository
    {
        private readonly ApplicationDbContext _context;

        public EquipmentHistoryRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EquipmentHistory>> GetAllAsync(
            DateTime? startDate,
            DateTime? endDate)
        {
            IQueryable<EquipmentHistory> query =
                _context.EquipmentHistories.AsNoTracking();

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

        public async Task<EquipmentHistory?> GetByIdAsync(int id)
        {
            return await _context.EquipmentHistories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<EquipmentHistory> CreateAsync(
            EquipmentHistory history)
        {
            _context.EquipmentHistories.Add(history);

            await _context.SaveChangesAsync();

            return history;
        }
    }
}
