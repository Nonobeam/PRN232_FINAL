using Model.Models;

public interface ITreatmentScheduleRepository
{
    Task<List<TreatmentSchedule>> GetAllAsync();
    Task<TreatmentSchedule> GetByIdAsync(int id);
    Task AddAsync(TreatmentSchedule schedule);
    void Update(TreatmentSchedule schedule);
    void Delete(TreatmentSchedule schedule);
    Task SaveAsync();
}