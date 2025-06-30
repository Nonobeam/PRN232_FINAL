using Microsoft.EntityFrameworkCore;
using Model.Models;
using Repository.Context;

namespace Repository.Implement
{
    public class TreatmentBookingRepository : ITreatmentBookingRepository
    {
        private readonly InfertilityTreatmentDBContext _context;

        public TreatmentBookingRepository(InfertilityTreatmentDBContext context)
        {
            _context = context;
        }

        public async Task<List<TreatmentBooking>> GetAllAsync()
        {
            return await _context.TreatmentBookings
                .OrderByDescending(b => b.BookingDate)
                .ToListAsync();
        }

        public async Task<List<TreatmentBooking>> GetAllByUserIdAsync(int userId)
        {
            return await _context.TreatmentBookings
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.BookingDate)
                .ToListAsync();
        }

        public async Task<List<TreatmentBooking>> GetAlByDoctorIdlAsync(int doctorId)
        {
            return await _context.TreatmentBookings
                .Where(b => b.DoctorId == doctorId)
                .OrderByDescending(b => b.BookingDate)
                .ToListAsync();
        }

        public async Task<List<TreatmentBooking>> GetAllByServiceIdAsync(int serviceId)
        {
            return await _context.TreatmentBookings
                .Where(b => b.ServiceId == serviceId)
                .OrderByDescending(b => b.BookingDate)
                .ToListAsync();
        }

        public async Task<TreatmentBooking> GetByIdAsync(int id)
        {
            return await _context.TreatmentBookings.FindAsync(id);
        }

        public async Task AddAsync(TreatmentBooking booking)
        {
            await _context.TreatmentBookings.AddAsync(booking);
        }

        public void Update(TreatmentBooking booking)
        {
            _context.TreatmentBookings.Update(booking);
        }

        public void Delete(TreatmentBooking booking)
        {
            _context.TreatmentBookings.Remove(booking);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}