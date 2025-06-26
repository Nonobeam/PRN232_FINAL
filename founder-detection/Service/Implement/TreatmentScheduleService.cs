using Model.Models;
using Repository;

namespace Service.Implement
{
    public class TreatmentScheduleService : ITreatmentScheduleService
    {
        private readonly ITreatmentScheduleRepository _repository;

        public TreatmentScheduleService(ITreatmentScheduleRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TreatmentSchedule>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TreatmentSchedule> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateAsync(TreatmentSchedule a)
        {
            await _repository.AddAsync(a);
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(TreatmentSchedule a)
        {
            _repository.Update(a);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(TreatmentSchedule a)
        {
            _repository.Delete(a);
            await _repository.SaveAsync();
        }
    }
}