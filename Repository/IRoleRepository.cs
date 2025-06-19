using Model.Models;

namespace Repository
{
    public interface IRoleRepository
    {
        Task<List<Role>> GetAllAsync();
        Task<Role> GetByIdAsync(int id);
        Task AddAsync(Role role);
        void Update(Role role);
        void Delete(Role role);
        Task SaveAsync();
    }
}