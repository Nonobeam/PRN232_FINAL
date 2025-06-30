using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Service;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostService _blogService;

        public BlogPostController(IBlogPostService blogService)
        {
            _blogService = blogService;
        }

        // GET: api/BlogPost
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogPost>>> GetAllBlogPosts()
        {
            return await _blogService.GetAllAsync();
        }

        // GET: api/BlogPost/user/1
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<BlogPost>>> GetAllBlogPostsByUserId(int userId)
        {
            return await _blogService.GetAllByUserIdAsync(userId);
        }

        // GET: api/BlogPost/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPost>> GetBlogPost(int id)
        {
            var blogPost = await _blogService.GetByIdAsync(id);

            return blogPost;
        }

        // PUT: api/BlogPost/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlogPost(int id, BlogPost blog)
        {
            if (id != blog.PostId) return BadRequest();

            await _blogService.UpdateAsync(blog);

            return NoContent();
        }

        // POST: api/BlogPost
        [HttpPost]
        public async Task<ActionResult<BlogPost>> PostBlogPost(BlogPost blog)
        {
            await _blogService.CreateAsync(blog);

            return CreatedAtAction("GetBlogPost", new { id = blog.PostId }, blog);
        }

        // DELETE: api/BlogPost/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogPost(BlogPost blog)
        {
            await _blogService.DeleteAsync(blog);

            return NoContent();
        }
    }
}
