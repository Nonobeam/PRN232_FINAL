using Microsoft.EntityFrameworkCore;
using Model.Models;
using Repository.Context;

namespace Repository.Implement
{
    public class MedicalRecordRepository : IMedicalRecordRepository
    {
        private readonly InfertilityTreatmentDBContext _context;

        public MedicalRecordRepository(InfertilityTreatmentDBContext context)
        {
            _context = context;
        }

        public async Task<List<MedicalRecord>> GetAllAsync()
        {
            return await _context.MedicalRecords.ToListAsync();
        }

        public async Task<MedicalRecord> GetByIdAsync(int id)
        {
            return await _context.MedicalRecords.FindAsync(id);
        }

        public async Task AddAsync(MedicalRecord record)
        {
            await _context.MedicalRecords.AddAsync(record);
        }

        public void Update(MedicalRecord record)
        {
            _context.MedicalRecords.Update(record);
        }

        public void Delete(MedicalRecord record)
        {
            _context.MedicalRecords.Remove(record);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}