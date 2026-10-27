using SolvefyTask.DTOs;
using SolvefyTask.Models;
using SolvefyTask.Repositories;

namespace SolvefyTask.Services
{
    public class TeamMemberService : ITeamMemberService
    {
        private readonly ITeamMemberRepository _repository;

        private static readonly string[] AllowedRoles =
        {
            "Field Staff",
            "Office Staff",
            "Admin"
        };

        private static readonly string[] AllowedStatuses =
        {
            "Active",
            "Inactive"
        };

        public TeamMemberService(ITeamMemberRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TeamMemberResponseDto>> GetAllAsync()
        {
            var members = await _repository.GetAllAsync();

            return members.Select(MapToDto).ToList();
        }

        public async Task<TeamMemberResponseDto?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid team member ID.");

            var member = await _repository.GetByIdAsync(id);

            return member == null ? null : MapToDto(member);
        }

        public async Task<TeamMemberResponseDto> CreateAsync(
            CreateTeamMemberDto dto)
        {
            ValidateFields(
                dto.Name,
                dto.Role,
                dto.Phone,
                dto.Email,
                dto.Status);

            var member = new TeamMember
            {
                Name = dto.Name.Trim(),
                Role = NormalizeRole(dto.Role),
                Phone = dto.Phone.Trim(),
                Email = dto.Email.Trim(),
                Status = NormalizeStatus(dto.Status)
            };

            var created = await _repository.CreateAsync(member);

            return MapToDto(created);
        }

        public async Task<TeamMemberResponseDto?> UpdateAsync(
            int id,
            UpdateTeamMemberDto dto)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid team member ID.");

            ValidateFields(
                dto.Name,
                dto.Role,
                dto.Phone,
                dto.Email,
                dto.Status);

            var updatedMember = new TeamMember
            {
                Name = dto.Name.Trim(),
                Role = NormalizeRole(dto.Role),
                Phone = dto.Phone.Trim(),
                Email = dto.Email.Trim(),
                Status = NormalizeStatus(dto.Status)
            };

            var updated = await _repository.UpdateAsync(
                id,
                updatedMember);

            return updated == null ? null : MapToDto(updated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid team member ID.");

            return await _repository.DeleteAsync(id);
        }

        private static void ValidateFields(
            string name,
            string role,
            string phone,
            string email,
            string status)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.");

            if (name.Trim().Length > 150)
                throw new ArgumentException(
                    "Name cannot exceed 150 characters.");

            if (string.IsNullOrWhiteSpace(role) ||
                !AllowedRoles.Contains(
                    role.Trim(),
                    StringComparer.OrdinalIgnoreCase))
            {
                throw new ArgumentException(
                    "Role must be Field Staff, Office Staff, or Admin.");
            }

            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentException("Phone is required.");

            if (phone.Trim().Length > 20)
                throw new ArgumentException(
                    "Phone cannot exceed 20 characters.");

            if (string.IsNullOrWhiteSpace(email) ||
                !new System.ComponentModel.DataAnnotations
                    .EmailAddressAttribute()
                    .IsValid(email.Trim()))
            {
                throw new ArgumentException(
                    "A valid email address is required.");
            }

            if (email.Trim().Length > 150)
                throw new ArgumentException(
                    "Email cannot exceed 150 characters.");

            if (string.IsNullOrWhiteSpace(status) ||
                !AllowedStatuses.Contains(
                    status.Trim(),
                    StringComparer.OrdinalIgnoreCase))
            {
                throw new ArgumentException(
                    "Status must be Active or Inactive.");
            }
        }

        private static string NormalizeRole(string role)
        {
            return AllowedRoles.First(
                x => x.Equals(
                    role.Trim(),
                    StringComparison.OrdinalIgnoreCase));
        }

        private static string NormalizeStatus(string status)
        {
            return AllowedStatuses.First(
                x => x.Equals(
                    status.Trim(),
                    StringComparison.OrdinalIgnoreCase));
        }

        private static TeamMemberResponseDto MapToDto(
            TeamMember member)
        {
            return new TeamMemberResponseDto
            {
                Id = member.Id,
                Name = member.Name,
                Role = member.Role,
                Phone = member.Phone,
                Email = member.Email,
                Status = member.Status
            };
        }
    }
}
