using Model.Models;

namespace Service.Implement;

public class TreatmentBookingService : ITreatmentBookingService
{
    private readonly ITreatmentBookingRepository _repository;

    public TreatmentBookingService(ITreatmentBookingRepository repository)
    {
        _repository = repository;
    }

    public Task<List<TreatmentBooking>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TreatmentBooking> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(TreatmentBooking a)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(TreatmentBooking a)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(TreatmentBooking a)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}