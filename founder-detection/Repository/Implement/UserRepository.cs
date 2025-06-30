using Microsoft.EntityFrameworkCore;
using Model.Models;
using Repository.Context;
using System.Security.Cryptography;

namespace Repository.Implement
{
    public class UserRepository : IUserRepository
    {
        private readonly InfertilityTreatmentDBContext _context;

        public UserRepository(InfertilityTreatmentDBContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task AddAsync(User user)
        {
            var salt = RandomNumberGenerator.GetBytes(16);
            var hash = new Rfc2898DeriveBytes(user.PasswordHash, salt, 10000, HashAlgorithmName.SHA256).GetBytes(32);
            var hashBytes = salt.Concat(hash).ToArray();
            user.PasswordHash = Convert.ToBase64String(hashBytes);
            await _context.Users.AddAsync(user);
        }


        public void Update(User user)
        {
            _context.Users.Update(user);
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByEmailAndPasswordAsync(string email, string password)
        {
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return null;

            var hashBytes = Convert.FromBase64String(user.PasswordHash);
            var salt = hashBytes.Take(16).ToArray();
            var storedHash = hashBytes.Skip(16).ToArray();

            var hash = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256).GetBytes(32);

            return hash.SequenceEqual(storedHash) ? user : null;
        }
    }
}