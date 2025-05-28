using Model.Models;

namespace Service.Implement;

public class ServicesService : IServicesService
{
    private readonly IServicesRepository _repository;

    public ServicesService(IServicesRepository repository)
    {
        _repository = repository;
    }

    public Task<List<Services>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    public Task<Services> GetByIdAsync(int id)
    {
        return _repository.GetByIdAsync(id);
    }

    public Task CreateAsync(Services service)
    {
        return _repository.AddAsync(service);
    }

    public Task UpdateAsync(Services service)
    {
        return _repository.UpdateAsync(service);
    }

    public Task DeleteAsync(Services service)
    {
        return _repository.DeleteAsync(service);
    }
}