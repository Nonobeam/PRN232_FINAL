using Model.Models;
using Repository;

namespace Service.Implement
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IBlogPostRepository _repository;

        public BlogPostService(IBlogPostRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<BlogPost>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<BlogPost> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateAsync(BlogPost a)
        {
            await _repository.AddAsync(a);
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(BlogPost a)
        {
            _repository.Update(a);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(BlogPost a)
        {
            _repository.Delete(a);
            await _repository.SaveAsync();
        }
    }
}