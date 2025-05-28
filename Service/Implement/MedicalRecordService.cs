using Model.Models;

namespace Service.Implement;

public class MedicalRecordService : IMedicalRecordService
{
    private readonly IMedicalRecordRepository _repository;

    public MedicalRecordService(IMedicalRecordRepository repository)
    {
        _repository = repository;
    }

    public Task<List<MedicalRecord>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<MedicalRecord> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(MedicalRecord a)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(MedicalRecord a)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(MedicalRecord a)
    {
        throw new NotImplementedException();
    }
}