using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolvefyTask.Services;

namespace SolvefyTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentHistoryController : ControllerBase
    {
        private readonly IEquipmentHistoryService _service;

        public EquipmentHistoryController(
            IEquipmentHistoryService service)
        {
            _service = service;
        }

        // GET: api/EquipmentHistory
        // Optional: ?startDate=2026-09-01&endDate=2026-09-30
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate)
        {
            try
            {
                var history = await _service.GetAllAsync(
                    startDate, endDate);

                return Ok(history);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/EquipmentHistory/1
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var record = await _service.GetByIdAsync(id);

                if (record == null)
                {
                    return NotFound(new
                    {
                        message = "Equipment history record not found."
                    });
                }

                return Ok(record);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
