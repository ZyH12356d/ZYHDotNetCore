using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZYHDotNetCore.Database.AppDbContextModels;
using ZYHDotNetCore.Domain.Features.Blog;

namespace ZYHDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogServiceController : ControllerBase
    {
        private readonly BlogServices _blogService;

        public BlogServiceController()
        {
            _blogService = new BlogServices();
        }
        [HttpGet]
        public IActionResult GetBlog()
        {
            var lst = _blogService.GetBlog(); 
            return Ok(lst);
        }
        [HttpGet("{id}")]
        public IActionResult GetBlogbyid(int id)
        {
            var item = _blogService.GetBlogById(id);
            if (item is not null)
            {
                return Ok(item);
            }
            else
            {
                return NotFound("There is no data");
            }

        }
        [HttpPost]
        public IActionResult CreateBlog(TblBlog blog)
        {
            if (blog == null)
                return BadRequest("Invalid data.");

            var result = _blogService.CreateBlog(blog);

            return CreatedAtAction(nameof(GetBlogbyid), new { id = blog.Id }, blog);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, TblBlog blog)
        {
            var item = _blogService.EditBlog(id , blog);            
            return Ok(item);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var item = _blogService.DeleteBlog(id);
            if (item is false)
            {
                return NotFound("There is no data");
            }
            return Ok("Deleted successfully");
        }
    }
}
