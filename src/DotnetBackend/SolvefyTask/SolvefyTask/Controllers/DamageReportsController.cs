using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolvefyTask.DTOs;
using SolvefyTask.Services;

namespace SolvefyTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DamageReportsController : ControllerBase
    {
        private readonly IDamageReportService _service;

        public DamageReportsController(IDamageReportService service)
        {
            _service = service;
        }

        // GET: api/DamageReports
        // Optional filters: search, status, startDate, endDate
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? search,
            [FromQuery] string? status,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate)
        {
            try
            {
                var reports = await _service.GetAllAsync(
                    search, status, startDate, endDate);

                return Ok(reports);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/DamageReports/1
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var report = await _service.GetByIdAsync(id);

                if (report == null)
                {
                    return NotFound(new
                    {
                        message = "Damage report not found."
                    });
                }

                return Ok(report);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST: api/DamageReports
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateDamageReportDto request)
        {
            try
            {
                var report = await _service.CreateAsync(request);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = report.Id },
                    report);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/DamageReports/1/resolve
        [HttpPut("{id:int}/resolve")]
        public async Task<IActionResult> Resolve(int id)
        {
            var result = await _service.ResolveAsync(id);

            if (!result.Success)
            {
                if (result.Message == "Damage report not found.")
                {
                    return NotFound(new { message = result.Message });
                }

                return BadRequest(new { message = result.Message });
            }

            return Ok(new
            {
                id,
                status = "Resolved",
                message = result.Message
            });
        }
    }
}
