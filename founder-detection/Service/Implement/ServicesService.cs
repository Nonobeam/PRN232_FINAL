using Model.Models;
using Repository;

namespace Service.Implement
{
    public class ServicesService : IServicesService
    {
        private readonly IServicesRepository _repository;

        public ServicesService(IServicesRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Services>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Services> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateAsync(Services a)
        {
            await _repository.AddAsync(a);
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(Services a)
        {
            await _repository.UpdateAsync(a);
        }

        public async Task DeleteAsync(Services a)
        {
            await _repository.DeleteAsync(a);
        }
    }
}