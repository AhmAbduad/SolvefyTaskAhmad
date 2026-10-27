using SolvefyTask.Models;

namespace SolvefyTask.Repositories
{
    public interface ITeamMemberRepository
    {
        Task<List<TeamMember>> GetAllAsync();

        Task<TeamMember?> GetByIdAsync(int id);

        Task<TeamMember> CreateAsync(TeamMember member);

        Task<TeamMember?> UpdateAsync(
            int id,
            TeamMember updatedMember);

        Task<bool> DeleteAsync(int id);
    }
}
