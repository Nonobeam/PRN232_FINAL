using Model.Models;

public interface ITreatmentBookingService
{
    Task<List<TreatmentBooking>> GetAllAsync();
    Task<TreatmentBooking> GetByIdAsync(int id);
    Task CreateAsync(TreatmentBooking a);
    Task UpdateAsync(TreatmentBooking a);
    Task DeleteAsync(TreatmentBooking a);
}