using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolvefyTask.Services;

namespace SolvefyTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyEquipmentController : ControllerBase
    {
        private readonly IMyEquipmentService _service;

        public MyEquipmentController(IMyEquipmentService service)
        {
            _service = service;
        }

        // GET: api/MyEquipment?assignedTo=Ahmad
        [HttpGet]
        public async Task<IActionResult> GetMyEquipment(
            [FromQuery] string assignedTo)
        {
            if (string.IsNullOrWhiteSpace(assignedTo))
            {
                return BadRequest(new
                {
                    message = "The assignedTo parameter is required."
                });
            }

            var equipment = await _service.GetMyEquipmentAsync(assignedTo);

            return Ok(equipment);
        }

        // GET: api/MyEquipment/1
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var equipment = await _service.GetByIdAsync(id);

            if (equipment == null)
            {
                return NotFound(new
                {
                    message = "Equipment checkout record not found."
                });
            }

            return Ok(equipment);
        }

        // PUT: api/MyEquipment/1/return
        [HttpPut("{id:int}/return")]
        public async Task<IActionResult> ReturnEquipment(int id)
        {
            var result = await _service.ReturnEquipmentAsync(id);

            if (!result.Success)
            {
                if (result.Message == "Equipment checkout record not found.")
                {
                    return NotFound(new { message = result.Message });
                }

                return BadRequest(new { message = result.Message });
            }

            return Ok(new
            {
                message = result.Message,
                id,
                status = "Returned"
            });
        }
    }
}
