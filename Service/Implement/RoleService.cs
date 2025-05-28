using Model.Models;

namespace Service.Implement;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _repository;

    public RoleService(IRoleRepository repository)
    {
        _repository = repository;
    }

    public Task<List<Role>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Role> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(Role a)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Role a)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Role a)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}