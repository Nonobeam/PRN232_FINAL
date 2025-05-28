using Model.Models;

namespace Service.Implement;

public class FeedbackService : IFeedbackService
{
    private readonly IFeedbackRepository _repository;

    public FeedbackService(IFeedbackRepository repository)
    {
        _repository = repository;
    }

    public Task<List<Feedback>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Feedback> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(Feedback a)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Feedback a)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Feedback a)
    {
        throw new NotImplementedException();
    }
}