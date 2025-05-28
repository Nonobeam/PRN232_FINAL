using Model.Models;

namespace Service;

public interface IMedicalRecordService
{
    Task<List<MedicalRecord>> GetAllAsync();
    Task<MedicalRecord> GetByIdAsync(int id);
    Task CreateAsync(MedicalRecord a);
    Task UpdateAsync(MedicalRecord a);
    Task DeleteAsync(MedicalRecord a);
}