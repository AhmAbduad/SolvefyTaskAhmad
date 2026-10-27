using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolvefyTask.DTOs;
using SolvefyTask.Services;

namespace SolvefyTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamMembersController : ControllerBase
    {
        private readonly ITeamMemberService _service;

        public TeamMembersController(ITeamMemberService service)
        {
            _service = service;
        }

        // GET: api/TeamMembers
        [HttpGet]
        public async Task<ActionResult<List<TeamMemberResponseDto>>>
            GetAll()
        {
            var members = await _service.GetAllAsync();

            return Ok(members);
        }

        // GET: api/TeamMembers/1
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TeamMemberResponseDto>>
            GetById(int id)
        {
            try
            {
                var member = await _service.GetByIdAsync(id);

                if (member == null)
                {
                    return NotFound(new
                    {
                        message = "Team member not found."
                    });
                }

                return Ok(member);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST: api/TeamMembers
        [HttpPost]
        public async Task<ActionResult<TeamMemberResponseDto>>
            Create([FromBody] CreateTeamMemberDto dto)
        {
            try
            {
                var member = await _service.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = member.Id },
                    member);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/TeamMembers/1
        [HttpPut("{id:int}")]
        public async Task<ActionResult<TeamMemberResponseDto>>
            Update(int id, [FromBody] UpdateTeamMemberDto dto)
        {
            try
            {
                var member = await _service.UpdateAsync(id, dto);

                if (member == null)
                {
                    return NotFound(new
                    {
                        message = "Team member not found."
                    });
                }

                return Ok(member);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/TeamMembers/1
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _service.DeleteAsync(id);

                if (!deleted)
                {
                    return NotFound(new
                    {
                        message = "Team member not found."
                    });
                }

                return Ok(new
                {
                    message = "Team member deleted successfully."
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
