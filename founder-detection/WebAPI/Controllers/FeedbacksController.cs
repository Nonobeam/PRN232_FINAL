using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Service;

namespace WebAPI.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class FeedbacksController : ControllerBase
{
    private readonly IFeedbackService _iFeedbackService;

    public FeedbacksController(IFeedbackService feedbackService)
    {
        _iFeedbackService = feedbackService;
    }

    // GET: api/Feedbacks
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Feedback>>> GetAllFeedbacks()
    {
        return await _iFeedbackService.GetAllAsync();
    }

    // GET: api/Feedbacks/doctor/2
    [HttpGet("doctor/{doctorId}")]
    public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedbacksByDoctorId(int doctorId)
    {
        return await _iFeedbackService.GetAllByDoctorIdAsync(doctorId);
    }

    // GET: api/Feedbacks/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Feedback>> GetFeedback(int id)
    {
        var feedback = await _iFeedbackService.GetByIdAsync(id);

        if (feedback == null)
        {
            return NotFound();
        }

        return feedback;
    }

    // PUT: api/Feedbacks/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutFeedback(int id, Feedback feedback)
    {
        if (id != feedback.FeedbackId)
        {
            return BadRequest();
        }

        await _iFeedbackService.UpdateAsync(feedback);

        return NoContent();
    }

    // POST: api/Feedbacks
    [HttpPost]
    public async Task<ActionResult<Feedback>> PostFeedback(Feedback feedback)
    {
        await _iFeedbackService.CreateAsync(feedback);

        return CreatedAtAction("GetFeedback", new { id = feedback.FeedbackId }, feedback);
    }

    // DELETE: api/Feedbacks/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFeedback(int id)
    {
        var feedback = await _iFeedbackService.GetByIdAsync(id);
        if (feedback == null)
        {
            return NotFound();
        }

        await _iFeedbackService.DeleteAsync(feedback);

        return NoContent();
    }
}
