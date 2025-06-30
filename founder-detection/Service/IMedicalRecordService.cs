using Model.Models;

namespace Service
{
    public interface IMedicalRecordService
    {
        Task<List<MedicalRecord>> GetAllAsync();
        Task<List<MedicalRecord>> GetAllByBookingIdAsync(int bookingId);
        Task<List<MedicalRecord>> GetAllByDoctorIdAsync(int doctorId);
        Task<MedicalRecord> GetByIdAsync(int id);
        Task CreateAsync(MedicalRecord a);
        Task UpdateAsync(MedicalRecord a);
        Task DeleteAsync(MedicalRecord a);
    }
}