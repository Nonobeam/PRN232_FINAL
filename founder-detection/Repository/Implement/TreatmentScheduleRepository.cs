using Microsoft.EntityFrameworkCore;
using Model.Models;
using Repository.Context;

namespace Repository.Implement
{
    public class TreatmentScheduleRepository : ITreatmentScheduleRepository
    {
        private readonly InfertilityTreatmentDBContext _context;

        public TreatmentScheduleRepository(InfertilityTreatmentDBContext context)
        {
            _context = context;
        }

        public async Task<List<TreatmentSchedule>> GetAllAsync()
        {
            return await _context.TreatmentSchedules
                .OrderByDescending(s => s.EventDate)
                .ToListAsync();
        }

        public async Task<List<TreatmentSchedule>> GetAllByBookingIdAsync(int bookingId)
        {
            return await _context.TreatmentSchedules
                .Where(s => s.BookingId == bookingId)
                .OrderByDescending(s => s.EventDate)
                .ToListAsync();
        }

        public async Task<TreatmentSchedule> GetByIdAsync(int id)
        {
            return await _context.TreatmentSchedules.FindAsync(id);
        }

        public async Task AddAsync(TreatmentSchedule schedule)
        {
            await _context.TreatmentSchedules.AddAsync(schedule);
        }

        public void Update(TreatmentSchedule schedule)
        {
            _context.TreatmentSchedules.Update(schedule);
        }

        public void Delete(TreatmentSchedule schedule)
        {
            _context.TreatmentSchedules.Remove(schedule);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}