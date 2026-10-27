using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolvefyTask.Models;
using SolvefyTask.Services;

namespace SolvefyTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentService _equipmentService;

        public EquipmentController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Equipment>>> GetAll()
        {
            var equipments = await _equipmentService.GetAllAsync();

            return Ok(equipments);
        }


        [HttpPost]
        public async Task<ActionResult<Equipment>> Insert(
            [FromBody] Equipment equipment)
        {
            if (string.IsNullOrWhiteSpace(equipment.Name))
            {
                return BadRequest("Equipment name is required.");
            }

            if (string.IsNullOrWhiteSpace(equipment.Category))
            {
                return BadRequest("Equipment category is required.");
            }

            var createdEquipment = await _equipmentService.InsertAsync(equipment);

            return CreatedAtAction(
                nameof(GetAll),
                new { id = createdEquipment.Id },
                createdEquipment
            );
        }
    }
}
