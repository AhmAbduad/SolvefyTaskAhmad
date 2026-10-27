using Microsoft.EntityFrameworkCore;
using SolvefyTask.Data;
using SolvefyTask.DTOs;
using SolvefyTask.Models;

namespace SolvefyTask.Repositories
{
    public class CheckoutRepository : ICheckoutRepository
    {
        private readonly ApplicationDbContext _context;

        public CheckoutRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Checkout> InsertAsync(Checkout checkout)
        {
            _context.Checkouts.Add(checkout);
            await _context.SaveChangesAsync();

            return checkout;
        }

        // GET: Equipment IDs and names
        public async Task<List<EquipmentOptionDto>> GetEquipmentOptionsAsync()
        {
            return await _context.Equipments
                .AsNoTracking()
                .Select(e => new EquipmentOptionDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Type = e.Type,
                    Category = e.Category
                })
                .OrderBy(e => e.Name)
                .ToListAsync();
        }

        // GET: Assignee for a particular equipment ID
        public async Task<string?> GetAssignedToByEquipmentIdAsync(
            int equipmentId)
        {
            return await _context.Equipments
                .AsNoTracking()
                .Where(e => e.Id == equipmentId)
                .Select(e => e.AssignedTo)
                .FirstOrDefaultAsync();
        }
    }
}
