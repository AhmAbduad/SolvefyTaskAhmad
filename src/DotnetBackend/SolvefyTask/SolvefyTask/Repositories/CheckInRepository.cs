using Microsoft.EntityFrameworkCore;
using SolvefyTask.Data;
using SolvefyTask.Models;

namespace SolvefyTask.Repositories
{
    public class CheckInRepository : ICheckInRepository
    {
        private readonly ApplicationDbContext _context;

        public CheckInRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Equipment>> GetEquipmentAsync()
        {
            return await _context.Equipments
                .AsNoTracking()
                .OrderBy(e => e.Name)
                .ToListAsync();
        }

        public async Task<Equipment?> GetEquipmentByIdAsync(
            int equipmentId)
        {
            return await _context.Equipments
                .FirstOrDefaultAsync(e => e.Id == equipmentId);
        }

        public async Task<CheckIn> CreateCheckInAsync(CheckIn checkIn)
        {
            await using var transaction =
                await _context.Database.BeginTransactionAsync();

            try
            {
                var equipment = await _context.Equipments
                    .FirstOrDefaultAsync(
                        e => e.Id == checkIn.EquipmentId);

                if (equipment == null)
                {
                    throw new InvalidOperationException(
                        "Equipment was not found.");
                }

                // Record the return history.
                checkIn.Equipment = equipment;
                checkIn.CreatedAt = DateTime.UtcNow;

                await _context.CheckIns.AddAsync(checkIn);

                // Update the current equipment status.
                equipment.Status =
                    checkIn.Condition.Equals(
                        "Damaged",
                        StringComparison.OrdinalIgnoreCase)
                    || checkIn.Condition.Equals(
                        "Needs Repair",
                        StringComparison.OrdinalIgnoreCase)
                    || checkIn.Condition.Equals(
                        "Poor",
                        StringComparison.OrdinalIgnoreCase)
                        ? "Under Repair"
                        : "Available";

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return checkIn;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
