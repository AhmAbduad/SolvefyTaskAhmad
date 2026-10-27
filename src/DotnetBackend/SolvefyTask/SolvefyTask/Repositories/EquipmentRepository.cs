using Microsoft.EntityFrameworkCore;
using SolvefyTask.Data;
using SolvefyTask.Models;

namespace SolvefyTask.Repositories
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly ApplicationDbContext _context;

        public EquipmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Equipment>> GetAllAsync()
        {
            return await _context.Equipments
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Equipment> InsertAsync(Equipment equipment)
        {
            _context.Equipments.Add(equipment);

            await _context.SaveChangesAsync();

            return equipment;
        }
    }
}
