using Model.Models;

namespace Service
{
    public interface IFeedbackService
    {
        Task<List<Feedback>> GetAllAsync();
        Task<Feedback> GetByIdAsync(int id);
        Task CreateAsync(Feedback a);
        Task UpdateAsync(Feedback a);
        Task DeleteAsync(Feedback a);
    }
}