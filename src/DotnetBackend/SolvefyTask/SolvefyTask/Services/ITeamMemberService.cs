using SolvefyTask.DTOs;

namespace SolvefyTask.Services
{
    public interface ITeamMemberService
    {
        Task<List<TeamMemberResponseDto>> GetAllAsync();

        Task<TeamMemberResponseDto?> GetByIdAsync(int id);

        Task<TeamMemberResponseDto> CreateAsync(
            CreateTeamMemberDto dto);

        Task<TeamMemberResponseDto?> UpdateAsync(
            int id,
            UpdateTeamMemberDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
