using Microsoft.EntityFrameworkCore;
using Model.Models;
using Repository.Context;

public class RoleRepository : IRoleRepository
{
    private readonly InfertilityTreatmentDBContext _context;

    public RoleRepository(InfertilityTreatmentDBContext context)
    {
        _context = context;
    }

    public async Task<List<Role>> GetAllAsync()
    {
        return await _context.Roles.ToListAsync();
    }

    public async Task<Role> GetByIdAsync(int id)
    {
        return await _context.Roles.FindAsync(id);
    }

    public async Task AddAsync(Role role)
    {
        await _context.Roles.AddAsync(role);
    }

    public void Update(Role role)
    {
        _context.Roles.Update(role);
    }

    public void Delete(Role role)
    {
        _context.Roles.Remove(role);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}