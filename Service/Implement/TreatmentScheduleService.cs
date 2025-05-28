using Model.Models;

namespace Service.Implement;

public class TreatmentScheduleService : ITreatmentScheduleService
{
    private readonly ITreatmentScheduleRepository _repository;

    public TreatmentScheduleService(ITreatmentScheduleRepository repository)
    {
        _repository = repository;
    }

    public Task<List<TreatmentSchedule>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TreatmentSchedule> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(TreatmentSchedule a)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(TreatmentSchedule a)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(TreatmentSchedule a)
    {
        throw new NotImplementedException();
    }
}