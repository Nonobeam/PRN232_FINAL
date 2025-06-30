using Model.Models;

namespace Service
{
    public interface IDoctorService
    {
        Task<List<Doctor>> GetAllAsync();
        Task<Doctor> GetByIdAsync(int id);
        Task<Doctor> GetByUserIdAsync(int userId);
        Task CreateAsync(Doctor a);
        Task UpdateAsync(Doctor a);
        Task DeleteAsync(Doctor a);
    }
}