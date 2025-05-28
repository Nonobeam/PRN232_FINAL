using Microsoft.AspNetCore.Mvc;
using Model.Models;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorController : ControllerBase
{
    private readonly IDoctorService _idoctorService;

    public DoctorController(IDoctorService doctorService)
    {
        _idoctorService = doctorService;
    }

    // GET: api/Doctor
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctor()
    {
        return await _idoctorService.GetAllAsync();
    }

    // GET: api/Doctor/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Doctor>> GetDoctor(int id)
    {
        var blogPost = await _idoctorService.GetByIdAsync(id);

        return blogPost;
    }

    // PUT: api/Doctor/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDoctor(int id, Doctor doctor)
    {
        if (id != doctor.DoctorId) return BadRequest();

        await _idoctorService.UpdateAsync(doctor);

        return NoContent();
    }

    // POST: api/Doctor
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Doctor>> PostDoctor(Doctor doctor)
    {
        await _idoctorService.CreateAsync(doctor);

        return CreatedAtAction("GetDoctor", new { id = doctor.DoctorId }, doctor);
    }

    // DELETE: api/Doctor/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDoctor(Doctor doctor)
    {
        await _idoctorService.DeleteAsync(doctor);

        return NoContent();
    }
}