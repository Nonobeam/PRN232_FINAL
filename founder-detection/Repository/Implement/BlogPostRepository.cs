using Microsoft.EntityFrameworkCore;
using Model.Models;
using Repository.Context;

namespace Repository.Implement
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly InfertilityTreatmentDBContext _context;

        public BlogPostRepository(InfertilityTreatmentDBContext context)
        {
            _context = context;
        }

        public async Task<List<BlogPost>> GetAllAsync()
        {
            return await _context.BlogPosts
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<BlogPost>> GetAllByUserIdAsync(int userId)
        {
            return await _context.BlogPosts
                .OrderByDescending(b => b.CreatedAt)
                .Where(b => b.CreatedBy == userId)
                .ToListAsync();
        }

        public async Task<BlogPost> GetByIdAsync(int id)
        {
            return await _context.BlogPosts.FindAsync(id);
        }

        public async Task AddAsync(BlogPost post)
        {
            await _context.BlogPosts.AddAsync(post);
        }

        public void Update(BlogPost post)
        {
            _context.BlogPosts.Update(post);
        }

        public void Delete(BlogPost post)
        {
            _context.BlogPosts.Remove(post);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}