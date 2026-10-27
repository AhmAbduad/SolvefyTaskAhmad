using Microsoft.EntityFrameworkCore;
using SolvefyTask.Data;
using SolvefyTask.Models;

namespace SolvefyTask.Repositories
{
    public class ApprovalRepository : IApprovalRepository
    {
        private readonly ApplicationDbContext _context;

        public ApprovalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ApprovalRequest>> GetAllAsync()
        {
            return await _context.ApprovalRequests
                .AsNoTracking()
                .Include(a => a.Equipment)
                .OrderByDescending(a => a.RequestedOn)
                .ToListAsync();
        }

        public async Task<ApprovalRequest?> GetByIdAsync(int id)
        {
            return await _context.ApprovalRequests
                .Include(a => a.Equipment)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<bool> EquipmentExistsAsync(int equipmentId)
        {
            return await _context.Equipments
                .AnyAsync(e => e.Id == equipmentId);
        }

        public async Task<ApprovalRequest> CreateAsync(
            ApprovalRequest request)
        {
            request.Status = "Pending Approval";
            request.RequestedOn = DateTime.UtcNow;
            request.Comments = null;
            request.DecisionAt = null;

            _context.ApprovalRequests.Add(request);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(request.Id) ?? request;
        }

        public async Task<ApprovalRequest?> DecideAsync(
            int id,
            string decision,
            string? comments)
        {
            var request = await _context.ApprovalRequests
                .Include(a => a.Equipment)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (request == null)
                return null;

            if (request.Status != "Pending Approval")
                throw new InvalidOperationException(
                    "This request has already been decided.");

            request.Status = decision;
            request.Comments = string.IsNullOrWhiteSpace(comments)
                ? null
                : comments.Trim();
            request.DecisionAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return request;
        }
    }
}
