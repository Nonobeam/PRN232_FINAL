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

        public Task<List<BlogPost>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BlogPost> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(BlogPost a)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(BlogPost a)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(BlogPost a)
        {
            throw new NotImplementedException();
        }
    }
}