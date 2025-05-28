using Model.Models;

public interface IDoctorService
{
    Task<List<Doctor>> GetAllAsync();
    Task<Doctor> GetByIdAsync(int id);
    Task CreateAsync(Doctor a);
    Task UpdateAsync(Doctor a);
    Task DeleteAsync(Doctor a);
}