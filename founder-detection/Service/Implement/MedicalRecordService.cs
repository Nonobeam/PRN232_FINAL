using Model.Models;
using Repository;

namespace Service.Implement
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IMedicalRecordRepository _repository;

        public MedicalRecordService(IMedicalRecordRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<MedicalRecord>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<MedicalRecord> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateAsync(MedicalRecord a)
        {
            await _repository.AddAsync(a);
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(MedicalRecord a)
        {
            _repository.Update(a);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(MedicalRecord a)
        {
            _repository.Delete(a);
            await _repository.SaveAsync();
        }
    }
}