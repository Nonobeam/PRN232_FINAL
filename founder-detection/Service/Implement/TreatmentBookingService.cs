using Model.Models;
using Repository;

namespace Service.Implement
{
    public class TreatmentBookingService : ITreatmentBookingService
    {
        private readonly ITreatmentBookingRepository _repository;

        public TreatmentBookingService(ITreatmentBookingRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TreatmentBooking>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TreatmentBooking> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateAsync(TreatmentBooking a)
        {
            await _repository.AddAsync(a);
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(TreatmentBooking a)
        {
            _repository.Update(a);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(TreatmentBooking a)
        {
            _repository.Delete(a);
            await _repository.SaveAsync();
        }
    }
}