using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Service;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordController : ControllerBase
    {
        private readonly IMedicalRecordService _iMedicalRecordService;

        public MedicalRecordController(IMedicalRecordService medicalRecordService)
        {
            _iMedicalRecordService = medicalRecordService;
        }

        // GET: api/MedicalRecord
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicalRecord>>> GetAllMedicalRecords()
        {
            return await _iMedicalRecordService.GetAllAsync();
        }

        // GET: api/MedicalRecord/booking/5
        [HttpGet("booking/{bookingId}")]
        public async Task<ActionResult<IEnumerable<MedicalRecord>>> GetMedicalRecordsByBookingId(int bookingId)
        {
            return await _iMedicalRecordService.GetAllByBookingIdAsync(bookingId);
        }

        // GET: api/MedicalRecord/doctor/5
        [HttpGet("doctor/{doctorId}")]
        public async Task<ActionResult<IEnumerable<MedicalRecord>>> GetMedicalRecordsByDoctorId(int id)
        {
            return await _iMedicalRecordService.GetAllByDoctorIdAsync(id);
        }

        // GET: api/MedicalRecord/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalRecord>> GetMedicalRecord(int id)
        {
            return await _iMedicalRecordService.GetByIdAsync(id);
        }

        // PUT: api/MedicalRecord/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicalRecord(int id, MedicalRecord medicalRecord)
        {
            if (id != medicalRecord.RecordId) return BadRequest();
            await _iMedicalRecordService.UpdateAsync(medicalRecord);
            return NoContent();
        }

        // POST: api/MedicalRecord
        [HttpPost]
        public async Task<ActionResult<MedicalRecord>> PostMedicalRecord(MedicalRecord medicalRecord)
        {
            await _iMedicalRecordService.CreateAsync(medicalRecord);
            return CreatedAtAction("GetMedicalRecord", new { id = medicalRecord.RecordId }, medicalRecord);
        }

        // DELETE: api/MedicalRecord/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicalRecord(MedicalRecord medicalRecord)
        {
            await _iMedicalRecordService.DeleteAsync(medicalRecord);
            return NoContent();
        }
    }
}
