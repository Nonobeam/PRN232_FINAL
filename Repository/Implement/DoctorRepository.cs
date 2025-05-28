using Microsoft.EntityFrameworkCore;
using Model.Models;
using Repository.Context;

public class DoctorRepository : IDoctorRepository
{
    private readonly InfertilityTreatmentDBContext _context;

    public DoctorRepository(InfertilityTreatmentDBContext context)
    {
        _context = context;
    }

    public async Task<List<Doctor>> GetAllAsync()
    {
        return await _context.Doctors.ToListAsync();
    }

    public async Task<Doctor> GetByIdAsync(int id)
    {
        return await _context.Doctors.FindAsync(id);
    }

    public async Task AddAsync(Doctor doctor)
    {
        await _context.Doctors.AddAsync(doctor);
    }

    public void Update(Doctor doctor)
    {
        _context.Doctors.Update(doctor);
    }

    public void Delete(Doctor doctor)
    {
        _context.Doctors.Remove(doctor);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}