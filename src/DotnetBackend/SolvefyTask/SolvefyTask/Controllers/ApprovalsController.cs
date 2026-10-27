using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolvefyTask.DTOs;
using SolvefyTask.Services;

namespace SolvefyTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApprovalsController : ControllerBase
    {
        private readonly IApprovalService _service;

        public ApprovalsController(IApprovalService service)
        {
            _service = service;
        }

        // GET: api/Approvals
        [HttpGet]
        public async Task<ActionResult<List<ApprovalResponseDto>>> GetAll()
        {
            var requests = await _service.GetAllAsync();
            return Ok(requests);
        }

        // GET: api/Approvals/1
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApprovalResponseDto>> GetById(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid approval ID." });

            var request = await _service.GetByIdAsync(id);

            if (request == null)
                return NotFound(new { message = "Approval request not found." });

            return Ok(request);
        }

        // POST: api/Approvals
        [HttpPost]
        public async Task<ActionResult<ApprovalResponseDto>> Create(
            [FromBody] CreateApprovalRequestDto dto)
        {
            try
            {
                var request = await _service.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = request.Id },
                    request);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/Approvals/1/approve
        [HttpPut("{id:int}/approve")]
        public async Task<ActionResult<ApprovalResponseDto>> Approve(
            int id,
            [FromBody] ApprovalDecisionDto? dto)
        {
            return await MakeDecision(id, "Approved", dto?.Comments);
        }

        // PUT: api/Approvals/1/reject
        [HttpPut("{id:int}/reject")]
        public async Task<ActionResult<ApprovalResponseDto>> Reject(
            int id,
            [FromBody] ApprovalDecisionDto? dto)
        {
            return await MakeDecision(id, "Rejected", dto?.Comments);
        }

        private async Task<ActionResult<ApprovalResponseDto>> MakeDecision(
            int id,
            string decision,
            string? comments)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid approval ID." });

            try
            {
                var request = await _service.DecideAsync(
                    id, decision, comments);

                if (request == null)
                    return NotFound(
                        new { message = "Approval request not found." });

                return Ok(request);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }
    }
}
