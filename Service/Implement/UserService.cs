using Model.Models;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task CreateAsync(User user)
    {
        await _repository.AddAsync(user);
        await _repository.SaveAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _repository.Update(user);
        await _repository.SaveAsync();
    }

    public async Task DeleteAsync(User user)
    {
        _repository.Delete(user);
        await _repository.SaveAsync();
    }
}