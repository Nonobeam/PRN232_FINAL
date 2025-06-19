using Model.Models;

namespace Service
{
    public interface IRoleService
    {
        Task<List<Role>> GetAllAsync();
        Task<Role> GetByIdAsync(int id);
        Task CreateAsync(Role a);
        Task UpdateAsync(Role a);
        Task DeleteAsync(Role a);
    }
}