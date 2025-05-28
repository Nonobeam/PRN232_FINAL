using Model.Models;

public interface IDoctorRepository
{
    Task<List<Doctor>> GetAllAsync();
    Task<Doctor> GetByIdAsync(int id);
    Task AddAsync(Doctor doctor);
    void Update(Doctor doctor);
    void Delete(Doctor doctor);
    Task SaveAsync();
}