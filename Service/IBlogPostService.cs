using Model.Models;

namespace Service;

public interface IBlogPostService
{
    Task<List<BlogPost>> GetAllAsync();
    Task<BlogPost> GetByIdAsync(int id);
    Task CreateAsync(BlogPost a);
    Task UpdateAsync(BlogPost a);
    Task DeleteAsync(BlogPost a);
}