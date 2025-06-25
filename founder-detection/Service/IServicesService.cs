using Model.Models;

namespace Service
{
    public interface IServicesService
    {
        Task<List<Services>> GetAllAsync();
        Task<Services> GetByIdAsync(int id);
        Task CreateAsync(Services a);
        Task UpdateAsync(Services a);
        Task DeleteAsync(Services a);
    }
}