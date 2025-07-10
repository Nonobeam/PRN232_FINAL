using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Service;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreatmentBookingController : ControllerBase
    {
        private readonly ITreatmentBookingService _iTreatmentBookingService;

        public TreatmentBookingController(ITreatmentBookingService treatmentBookingService)
        {
            _iTreatmentBookingService = treatmentBookingService;
        }

        // GET: api/TreatmentBooking
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TreatmentBooking>>> GetAllTreatmentBookings()
        {
            return await _iTreatmentBookingService.GetAllAsync();
        }

        // GET: api/TreatmentBooking/user/5
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<TreatmentBooking>>> GetTreatmentBookingsByUserId(int userId)
        {
            return await _iTreatmentBookingService.GetAllByUserIdAsync(userId);
        }

        // GET: api/TreatmentBooking/doctor/5
        [HttpGet("doctor/{doctorId}")]
        public async Task<ActionResult<IEnumerable<TreatmentBooking>>> GetTreatmentBookingByDoctorId(int doctorId)
        {
            return await _iTreatmentBookingService.GetAlByDoctorIdlAsync(doctorId);
        }

        // GET: api/TreatmentBooking/service/5
        [HttpGet("service/{serviceId}")]
        public async Task<ActionResult<IEnumerable<TreatmentBooking>>> GetTreatmentBookingByServiceId(int serviceId)
        {
            return await _iTreatmentBookingService.GetAllByServiceIdAsync(serviceId);
        }

        // GET: api/TreatmentBooking/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TreatmentBooking>> GetTreatmentBooking(int id)
        {
            var booking = await _iTreatmentBookingService.GetByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return booking;
        }

        // PUT: api/TreatmentBooking/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTreatmentBooking(int id, TreatmentBooking treatmentBooking)
        {
            if (id != treatmentBooking.BookingId) return BadRequest();

            await _iTreatmentBookingService.UpdateAsync(treatmentBooking);

            return NoContent();
        }

        // POST: api/TreatmentBooking
        [HttpPost]
        public async Task<ActionResult<TreatmentBooking>> PostTreatmentBooking(TreatmentBooking treatmentBooking)
        {
            await _iTreatmentBookingService.CreateAsync(treatmentBooking);

            return Ok(new { message = "Booking created successfully", bookingId = treatmentBooking.BookingId });
        }

        // DELETE: api/TreatmentBooking/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTreatmentBooking(TreatmentBooking treatmentBooking)
        {
            await _iTreatmentBookingService.DeleteAsync(treatmentBooking);

            return NoContent();
        }
    }
}
