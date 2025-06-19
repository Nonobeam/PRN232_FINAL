using Model.Models;

namespace Service
{
    public interface ITreatmentScheduleService
    {
        Task<List<TreatmentSchedule>> GetAllAsync();
        Task<TreatmentSchedule> GetByIdAsync(int id);
        Task CreateAsync(TreatmentSchedule a);
        Task UpdateAsync(TreatmentSchedule a);
        Task DeleteAsync(TreatmentSchedule a);
    }
}