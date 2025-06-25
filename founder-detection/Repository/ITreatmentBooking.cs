using Model.Models;

namespace Repository
{
    public interface ITreatmentBookingRepository
    {
        Task<List<TreatmentBooking>> GetAllAsync();
        Task<TreatmentBooking> GetByIdAsync(int id);
        Task AddAsync(TreatmentBooking booking);
        void Update(TreatmentBooking booking);
        void Delete(TreatmentBooking booking);
        Task SaveAsync();
    }
}