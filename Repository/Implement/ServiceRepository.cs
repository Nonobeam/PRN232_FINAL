using Microsoft.EntityFrameworkCore;
using Model.Models;
using Repository.Context;

public class ServicesRepository : IServicesRepository
{
    private readonly InfertilityTreatmentDBContext _context;

    public ServicesRepository(InfertilityTreatmentDBContext context)
    {
        _context = context;
    }

    public async Task<List<Services>> GetAllAsync()
    {
        return await _context.Services.ToListAsync();
    }

    public async Task<Services> GetByIdAsync(int id)
    {
        return await _context.Services.FindAsync(id);
    }

    public async Task AddAsync(Services service)
    {
        await _context.Services.AddAsync(service);
    }

    public async Task UpdateAsync(Services service)
    {
        _context.Services.Update(service);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Services service)
    {
        _context.Services.Remove(service);
        await _context.SaveChangesAsync();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}