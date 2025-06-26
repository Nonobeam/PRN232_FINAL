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

        public async Task<Feedback> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateAsync(Feedback a)
        {
            await _repository.AddAsync(a);
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(Feedback a)
        {
            _repository.Update(a);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(Feedback a)
        {
            _repository.Delete(a);
            await _repository.SaveAsync();
        }
    }
}