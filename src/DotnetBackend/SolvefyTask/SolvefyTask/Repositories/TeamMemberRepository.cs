using Microsoft.EntityFrameworkCore;
using SolvefyTask.Data;
using SolvefyTask.Models;

namespace SolvefyTask.Repositories
{
    public class TeamMemberRepository : ITeamMemberRepository
    {
        private readonly ApplicationDbContext _context;

        public TeamMemberRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TeamMember>> GetAllAsync()
        {
            return await _context.TeamMembers
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<TeamMember?> GetByIdAsync(int id)
        {
            return await _context.TeamMembers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TeamMember> CreateAsync(TeamMember member)
        {
            _context.TeamMembers.Add(member);

            await _context.SaveChangesAsync();

            return member;
        }

        public async Task<TeamMember?> UpdateAsync(
            int id,
            TeamMember updatedMember)
        {
            var existingMember = await _context.TeamMembers
                .FirstOrDefaultAsync(x => x.Id == id);

            if (existingMember == null)
                return null;

            existingMember.Name = updatedMember.Name;
            existingMember.Role = updatedMember.Role;
            existingMember.Phone = updatedMember.Phone;
            existingMember.Email = updatedMember.Email;
            existingMember.Status = updatedMember.Status;

            await _context.SaveChangesAsync();

            return existingMember;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var member = await _context.TeamMembers
                .FirstOrDefaultAsync(x => x.Id == id);

            if (member == null)
                return false;

            _context.TeamMembers.Remove(member);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
