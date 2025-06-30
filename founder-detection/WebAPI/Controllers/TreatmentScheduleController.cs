using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Service;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreatmentScheduleController : ControllerBase
    {
        private readonly ITreatmentScheduleService _iTreatmentScheduleService;

        public TreatmentScheduleController(ITreatmentScheduleService treatmentScheduleService)
        {
            _iTreatmentScheduleService = treatmentScheduleService;
        }

        // GET: api/TreatmentSchedule
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TreatmentSchedule>>> GetAllTreatmentSchedules()
        {
            return await _iTreatmentScheduleService.GetAllAsync();
        }

        // GET: api/TreatmentSchedule/booking/5
        [HttpGet("booking/{bookingId}")]
        public async Task<ActionResult<IEnumerable<TreatmentSchedule>>> GetTreatmentSchedulesByBookingId(int bookingId)
        {
            return await _iTreatmentScheduleService.GetAllByBookingIdAsync(bookingId);
        }

        // GET: api/TreatmentSchedule/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TreatmentSchedule>> GetTreatmentSchedule(int id)
        {
            return await _iTreatmentScheduleService.GetByIdAsync(id);
        }

        // PUT: api/TreatmentSchedule/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTreatmentSchedule(int id, TreatmentSchedule treatmentSchedule)
        {
            if (id != treatmentSchedule.ScheduleId) return BadRequest();
            await _iTreatmentScheduleService.UpdateAsync(treatmentSchedule);
            return NoContent();
        }

        // POST: api/TreatmentSchedule
        [HttpPost]
        public async Task<ActionResult<TreatmentSchedule>> PostTreatmentSchedule(TreatmentSchedule treatmentSchedule)
        {
            await _iTreatmentScheduleService.CreateAsync(treatmentSchedule);
            return CreatedAtAction("GetTreatmentSchedule", new { id = treatmentSchedule.ScheduleId }, treatmentSchedule);
        }

        // DELETE: api/TreatmentSchedule/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTreatmentSchedule(TreatmentSchedule treatmentSchedule)
        {
            await _iTreatmentScheduleService.DeleteAsync(treatmentSchedule);
            return NoContent();
        }
    }
}
