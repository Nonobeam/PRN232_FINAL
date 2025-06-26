using Model.Models;
using Repository;

namespace Service.Implement
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repository;

        public DoctorService(IDoctorRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Doctor>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Doctor> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateAsync(Doctor a)
        {
            await _repository.AddAsync(a);
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(Doctor a)
        {
            _repository.Update(a);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(Doctor a)
        {
            _repository.Delete(a);
            await _repository.SaveAsync();
        }
    }
}