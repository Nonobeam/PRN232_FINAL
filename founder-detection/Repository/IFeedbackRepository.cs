using Model.Models;

namespace Repository
{
    public interface IFeedbackRepository
    {
        Task<List<Feedback>> GetAllAsync();
        Task<List<Feedback>> GetAllByDoctorIdAsync(int doctorId);
        Task<Feedback> GetByIdAsync(int id);
        Task AddAsync(Feedback feedback);
        Task UpdateAsync(Feedback feedback);
        Task DeleteAsync(Feedback feedback);
    }
}