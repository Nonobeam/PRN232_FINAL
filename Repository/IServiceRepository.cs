using Model.Models;

public interface IServicesRepository
{
    Task<List<Services>> GetAllAsync();
    Task<Services> GetByIdAsync(int id);
    Task AddAsync(Services service);
    Task UpdateAsync(Services service);
    Task DeleteAsync(Services service);
    Task SaveAsync();
}