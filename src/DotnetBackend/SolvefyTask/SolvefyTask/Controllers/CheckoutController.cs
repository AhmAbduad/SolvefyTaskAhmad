using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolvefyTask.DTOs;
using SolvefyTask.Models;
using SolvefyTask.Services;

namespace SolvefyTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly ICheckoutService _service;
        private readonly IEquipmentHistoryService _historyService;
        public CheckoutController(ICheckoutService service,IEquipmentHistoryService historyService)
        {
            _service = service;
            _historyService = historyService;
        }


        // GET: api/Checkout/equipment
        [HttpGet("equipment")]
        public async Task<ActionResult<List<EquipmentOptionDto>>>
            GetEquipmentOptions()
        {
            var equipment = await _service.GetEquipmentOptionsAsync();
            return Ok(equipment);
        }

        // GET: api/Checkout/assigned-to/2
        [HttpGet("assigned-to/{equipmentId:int}")]
        public async Task<ActionResult<string>> GetAssignedTo(int equipmentId)
        {
            var assignedTo =
                await _service.GetAssignedToByEquipmentIdAsync(equipmentId);

            if (assignedTo == null)
            {
                return NotFound("No assigned team member found for this equipment.");
            }

            return Ok(assignedTo);
        }



        [HttpPost]
        public async Task<ActionResult<Checkout>> Create([FromBody] Checkout checkout)
        {
            if (string.IsNullOrWhiteSpace(checkout.EquipmentName) ||
                string.IsNullOrWhiteSpace(checkout.AssignedTo))
            {
                return BadRequest(
                    "Equipment and assigned team member are required.");
            }

            if (checkout.ExpectedReturnDate == default)
            {
                return BadRequest("Expected return date is required.");
            }

            if (checkout.ExpectedReturnDate.Date < DateTime.UtcNow.Date)
            {
                return BadRequest("Expected return date cannot be in the past.");
            }

            checkout.Purpose ??= string.Empty;

            var created = await _service.CreateAsync(checkout);

            await _historyService.RecordActivityAsync(
                equipment: checkout.EquipmentName,
                action: "Checked Out",
                person: checkout.AssignedTo,
                remarks: checkout.Purpose
            );
            return CreatedAtAction(
                nameof(Create),
                new { id = created.Id },
                created);
        }
    }
}
