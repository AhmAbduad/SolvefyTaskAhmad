using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolvefyTask.Models;
using SolvefyTask.Services;

namespace SolvefyTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckInController : ControllerBase
    {
        private readonly ICheckInService _service;

        public CheckInController(ICheckInService service)
        {
            _service = service;
        }

        // GET: /api/CheckIn/equipment
        [HttpGet("equipment")]
        public async Task<IActionResult> GetEquipment()
        {
            var equipment = await _service.GetEquipmentAsync();

            var result = equipment.Select(e => new
            {
                id = e.Id,
                name = e.Name,
                serial = e.SerialNumber,
                type = e.Type,
                status = e.Status
            });

            return Ok(result);
        }

        // GET: /api/CheckIn/equipment/1
        [HttpGet("equipment/{equipmentId:int}")]
        public async Task<IActionResult> GetEquipmentById(
            int equipmentId)
        {
            var equipment =
                await _service.GetEquipmentByIdAsync(equipmentId);

            if (equipment == null)
            {
                return NotFound("Equipment was not found.");
            }

            return Ok(new
            {
                id = equipment.Id,
                name = equipment.Name,
                serial = equipment.SerialNumber,
                type = equipment.Type,
                status = equipment.Status
            });
        }

        // POST: /api/CheckIn
        [HttpPost]
        public async Task<IActionResult> CreateCheckIn(
            [FromBody] CheckIn checkIn)
        {
            if (checkIn.EquipmentId <= 0)
            {
                return BadRequest(new
                {
                    message = "A valid equipment ID is required."
                });
            }

            var validConditions = new[]
            {
                "Good",
                "Fair",
                "Damaged",
                "Needs Repair",
                "Poor"
            };

            if (!validConditions.Contains(
                checkIn.Condition,
                StringComparer.OrdinalIgnoreCase))
            {
                return BadRequest(new
                {
                    message = "Invalid equipment condition."
                });
            }

            if (checkIn.ReturnDate.Date > DateTime.Today.AddYears(5))
            {
                return BadRequest(new
                {
                    message = "The return date is invalid."
                });
            }

            try
            {
                var result =
                    await _service.CreateCheckInAsync(checkIn);

                return CreatedAtAction(
                    nameof(GetEquipmentById),
                    new { equipmentId = result.EquipmentId },
                    new
                    {
                        id = result.Id,
                        equipmentId = result.EquipmentId,
                        returnDate = result.ReturnDate,
                        returnTime = result.ReturnTime.ToString(@"hh\:mm"),
                        condition = result.Condition,
                        notes = result.Notes,
                        status = result.Equipment?.Status,
                        createdAt = result.CreatedAt
                    });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
