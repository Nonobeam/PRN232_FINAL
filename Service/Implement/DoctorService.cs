using Model.Models;

namespace Service.Implement;

public class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _repository;

    public DoctorService(IDoctorRepository repository)
    {
        _repository = repository;
    }

    public Task<List<Doctor>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Doctor> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(Doctor a)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Doctor a)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Doctor a)
    {
        throw new NotImplementedException();
    }
}