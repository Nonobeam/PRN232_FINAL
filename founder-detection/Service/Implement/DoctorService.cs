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

        public async Task<Doctor> GetByUserIdAsync(int userId)
        {
            return await _repository.GetByUserIdAsync(userId);
        }

        public async Task CreateAsync(Doctor a)
        {
            await _repository.AddAsync(a);
        }

        public async Task UpdateAsync(Doctor a)
        {
            await _repository.UpdateAsync(a);
        }

        public async Task DeleteAsync(Doctor a)
        {
            await _repository.DeleteAsync(a);
        }
    }
}