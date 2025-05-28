using Model.Models;

public interface IBlogPostRepository
{
    Task<List<BlogPost>> GetAllAsync();
    Task<BlogPost> GetByIdAsync(int id);
    Task AddAsync(BlogPost post);
    void Update(BlogPost post);
    void Delete(BlogPost post);
    Task SaveAsync();
}