using Model.Models;

namespace Repository
{
    public interface IMedicalRecordRepository
    {
        Task<List<MedicalRecord>> GetAllAsync();
        Task<List<MedicalRecord>> GetAllByBookingIdAsync(int bookingId);
        Task<List<MedicalRecord>> GetAllByDoctorIdAsync(int doctorId);
        Task<MedicalRecord> GetByIdAsync(int id);
        Task AddAsync(MedicalRecord record);
        void Update(MedicalRecord record);
        void Delete(MedicalRecord record);
        Task SaveAsync();
    }
}