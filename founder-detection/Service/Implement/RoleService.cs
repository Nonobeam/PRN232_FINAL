using Model.Models;
using Repository;

namespace Service.Implement
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;

        public RoleService(IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Role>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Role> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateAsync(Role a)
        {
            await _repository.AddAsync(a);
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(Role a)
        {
            _repository.Update(a);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(Role a)
        {
            _repository.Delete(a);
            await _repository.SaveAsync();
        }
    }
}