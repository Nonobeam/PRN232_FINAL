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
    private readonly IFeedbackService _ifeedbackService;

    public FeedbacksController(IFeedbackService feedbackService)
    {
        _ifeedbackService = feedbackService;
    }

    // GET: api/Feedbacks
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedbacks()
    {
        return await _ifeedbackService.GetAllAsync();
    }

    // GET: api/Feedbacks/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Feedback>> GetFeedback(int id)
    {
        var feedback = await _ifeedbackService.GetByIdAsync(id);

        if (feedback == null)
        {
            return NotFound();
        }

        return feedback;
    }

    // PUT: api/Feedbacks/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutFeedback(int id, Feedback feedback)
    {
        if (id != feedback.FeedbackId)
        {
            return BadRequest();
        }

        await _ifeedbackService.UpdateAsync(feedback);

        return NoContent();
    }

    // POST: api/Feedbacks
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Feedback>> PostFeedback(Feedback feedback)
    {
        await _ifeedbackService.CreateAsync(feedback);

        return CreatedAtAction("GetFeedback", new { id = feedback.FeedbackId }, feedback);
    }

    // DELETE: api/Feedbacks/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFeedback(int id)
    {
        var feedback = await _ifeedbackService.GetByIdAsync(id);
        if (feedback == null)
        {
            return NotFound();
        }

        await _ifeedbackService.DeleteAsync(feedback);

        return NoContent();
    }
}
