using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Service;
using WebAPI.DTO;
using AutoMapper;

namespace WebAPI.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class DoctorController : ControllerBase
{
    private readonly IDoctorService _iDoctorService;
    private readonly IMapper _mapper;

    public DoctorController(IDoctorService doctorService, IMapper mapper)
    {
        _iDoctorService = doctorService;
        _mapper = mapper;
    }

    // GET: api/Doctor
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<DoctorDTO>>> GetAllDoctors()
    {
        var doctors = await _iDoctorService.GetAllAsync();
        var doctorDTOs = _mapper.Map<List<DoctorDTO>>(doctors);
        return Ok(doctorDTOs);
    }

    // GET: api/Doctor/5
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<DoctorDTO>> GetDoctor(int id)
    {
        var doctor = await _iDoctorService.GetByIdAsync(id);
        if (doctor == null)
        {
            return NotFound();
        }

        var doctorDTO = _mapper.Map<DoctorDTO>(doctor);
        return Ok(doctorDTO);
    }

    // GET: api/Doctor/user/5
    [HttpGet("user/{userId}")]
    [AllowAnonymous]
    public async Task<ActionResult<DoctorDTO>> GetDoctorByUserId(int userId)
    {
        var doctor = await _iDoctorService.GetByUserIdAsync(userId);
        if (doctor == null)
        {
            return NotFound();
        }

        var doctorDTO = _mapper.Map<DoctorDTO>(doctor);
        return Ok(doctorDTO);
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