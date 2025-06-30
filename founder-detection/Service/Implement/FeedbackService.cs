using Model.Models;
using Repository;

namespace Service.Implement
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _repository;

        public FeedbackService(IFeedbackRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Feedback>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<List<Feedback>> GetAllByDoctorIdAsync(int doctorId)
        {
            return await _repository.GetAllByDoctorIdAsync(doctorId);
        }

        public async Task<Feedback> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateAsync(Feedback a)
        {
            await _repository.AddAsync(a);
        }

        public async Task UpdateAsync(Feedback a)
        {
            await _repository.UpdateAsync(a);
        }

        public async Task DeleteAsync(Feedback a)
        {
            await _repository.DeleteAsync(a);
        }
    }
}