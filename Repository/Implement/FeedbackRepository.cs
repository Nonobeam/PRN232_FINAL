using Microsoft.EntityFrameworkCore;
using Model.Models;
using Repository.Context;

public class FeedbackRepository : IFeedbackRepository
{
    private readonly InfertilityTreatmentDBContext _context;

    public FeedbackRepository(InfertilityTreatmentDBContext context)
    {
        _context = context;
    }

    public async Task<List<Feedback>> GetAllAsync()
    {
        return await _context.Feedbacks.ToListAsync();
    }

    public async Task<Feedback> GetByIdAsync(int id)
    {
        return await _context.Feedbacks.FindAsync(id);
    }

    public async Task AddAsync(Feedback feedback)
    {
        await _context.Feedbacks.AddAsync(feedback);
    }

    public void Update(Feedback feedback)
    {
        _context.Feedbacks.Update(feedback);
    }

    public void Delete(Feedback feedback)
    {
        _context.Feedbacks.Remove(feedback);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}