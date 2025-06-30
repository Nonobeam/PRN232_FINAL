using Model.Models;

namespace Repository
{
    public interface IBlogPostRepository
    {
        Task<List<BlogPost>> GetAllAsync();
        Task<List<BlogPost>> GetAllByUserIdAsync(int userId);
        Task<BlogPost> GetByIdAsync(int id);
        Task AddAsync(BlogPost post);
        void Update(BlogPost post);
        void Delete(BlogPost post);
        Task SaveAsync();
    }
}