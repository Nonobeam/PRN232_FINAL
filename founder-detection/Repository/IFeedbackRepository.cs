using Model.Models;

namespace Repository
{
    public interface IFeedbackRepository
    {
        Task<List<Feedback>> GetAllAsync();
        Task<Feedback> GetByIdAsync(int id);
        Task AddAsync(Feedback feedback);
        void Update(Feedback feedback);
        void Delete(Feedback feedback);
        Task SaveAsync();
    }
}