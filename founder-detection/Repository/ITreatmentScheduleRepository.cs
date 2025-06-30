using Model.Models;

namespace Repository
{
    public interface ITreatmentScheduleRepository
    {
        Task<List<TreatmentSchedule>> GetAllAsync();
        Task<List<TreatmentSchedule>> GetAllByBookingIdAsync(int bookingId);
        Task<TreatmentSchedule> GetByIdAsync(int id);
        Task AddAsync(TreatmentSchedule schedule);
        void Update(TreatmentSchedule schedule);
        void Delete(TreatmentSchedule schedule);
        Task SaveAsync();
    }
}