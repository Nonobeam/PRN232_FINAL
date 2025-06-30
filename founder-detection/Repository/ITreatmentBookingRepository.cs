using Model.Models;

namespace Repository
{
    public interface ITreatmentBookingRepository
    {
        Task<List<TreatmentBooking>> GetAllAsync();
        Task<List<TreatmentBooking>> GetAllByUserIdAsync(int userId);
        Task<List<TreatmentBooking>> GetAlByDoctorIdlAsync(int doctorId);
        Task<List<TreatmentBooking>> GetAllByServiceIdAsync(int serviceId);
        Task<TreatmentBooking> GetByIdAsync(int id);
        Task AddAsync(TreatmentBooking booking);
        void Update(TreatmentBooking booking);
        void Delete(TreatmentBooking booking);
        Task SaveAsync();
    }
}