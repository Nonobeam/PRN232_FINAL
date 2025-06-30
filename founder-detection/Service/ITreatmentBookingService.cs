using Model.Models;

namespace Service
{
    public interface ITreatmentBookingService
    {
        Task<List<TreatmentBooking>> GetAllAsync();
        Task<List<TreatmentBooking>> GetAllByUserIdAsync(int userId);
        Task<List<TreatmentBooking>> GetAlByDoctorIdlAsync(int doctorId);
        Task<List<TreatmentBooking>> GetAllByServiceIdAsync(int serviceId);
        Task<TreatmentBooking> GetByIdAsync(int id);
        Task CreateAsync(TreatmentBooking a);
        Task UpdateAsync(TreatmentBooking a);
        Task DeleteAsync(TreatmentBooking a);
    }
}