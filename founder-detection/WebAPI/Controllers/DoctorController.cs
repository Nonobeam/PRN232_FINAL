using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Service;

namespace WebAPI.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class DoctorController : ControllerBase
{
    private readonly IDoctorService _iDoctorService;

    public DoctorController(IDoctorService doctorService)
    {
        _iDoctorService = doctorService;
    }

    // GET: api/Doctor
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Doctor>>> GetAllDoctors()
    {
        return await _iDoctorService.GetAllAsync();
    }

    // GET: api/Doctor/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Doctor>> GetDoctor(int id)
    {
        return await _iDoctorService.GetByIdAsync(id);
    }

    // GET: api/Doctor/user/5
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<Doctor>> GetDoctorByUserId(int userId)
    {
        return await _iDoctorService.GetByIdAsync(userId);
    }

    // PUT: api/Doctor/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDoctor(int id, Doctor doctor)
    {
        if (id != doctor.DoctorId) return BadRequest();
        await _iDoctorService.UpdateAsync(doctor);
        return NoContent();
    }

    // POST: api/Doctor
    [HttpPost]
    public async Task<ActionResult<Doctor>> PostDoctor(Doctor doctor)
    {
        await _iDoctorService.CreateAsync(doctor);
        return CreatedAtAction("GetDoctor", new { id = doctor.DoctorId }, doctor);
    }

    // DELETE: api/Doctor/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDoctor(Doctor doctor)
    {
        await _iDoctorService.DeleteAsync(doctor);
        return NoContent();
    }
}