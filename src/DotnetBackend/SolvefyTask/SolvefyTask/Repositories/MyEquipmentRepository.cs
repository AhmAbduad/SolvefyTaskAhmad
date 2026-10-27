using Microsoft.EntityFrameworkCore;
using SolvefyTask.Data;
using SolvefyTask.DTOs;

namespace SolvefyTask.Repositories
{
    public class MyEquipmentRepository : IMyEquipmentRepository
    {
        private readonly ApplicationDbContext _context;

        public MyEquipmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MyEquipmentResponseDto>>
            GetMyEquipmentAsync(string assignedTo)
        {
            var today = DateTime.UtcNow.Date;

            return await _context.Checkouts
                .AsNoTracking()
                .Where(x =>
                    x.AssignedTo == assignedTo &&
                    x.Status != "Returned")
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new MyEquipmentResponseDto
                {
                    Id = x.Id,
                    EquipmentName = x.EquipmentName,
                    CheckedOutDate = x.CreatedAt,
                    ExpectedReturnDate = x.ExpectedReturnDate,
                    Status = x.ExpectedReturnDate.Date < today
                        ? "Overdue"
                        : "Checked Out"
                })
                .ToListAsync();
        }

        public async Task<MyEquipmentResponseDto?> GetByIdAsync(int id)
        {
            var today = DateTime.UtcNow.Date;

            return await _context.Checkouts
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new MyEquipmentResponseDto
                {
                    Id = x.Id,
                    EquipmentName = x.EquipmentName,
                    CheckedOutDate = x.CreatedAt,
                    ExpectedReturnDate = x.ExpectedReturnDate,
                    Status = x.Status == "Returned"
                        ? "Returned"
                        : x.ExpectedReturnDate.Date < today
                            ? "Overdue"
                            : "Checked Out"
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> ReturnEquipmentAsync(int id)
        {
            var checkout = await _context.Checkouts
                .FirstOrDefaultAsync(x => x.Id == id);

            if (checkout == null)
            {
                return false;
            }

            checkout.Status = "Returned";

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
