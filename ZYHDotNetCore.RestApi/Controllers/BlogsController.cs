using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZYHDotNetCore.Database.AppDbContextModels;

namespace ZYHDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        AppDbContext _context = new AppDbContext();
        [HttpGet]
        public IActionResult GetBlog()
        {
            var lst = _context.TblBlogs.AsNoTracking().Where(x => x.DeleteFlag == 0).ToList();
            return Ok(lst);
        }
        [HttpGet("{id}")]
        public IActionResult GetBlogbyid(int id)
        {
            var item = _context.TblBlogs.AsNoTracking().FirstOrDefault(x => x.Id == id && x.DeleteFlag == 0);
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

            _context.TblBlogs.Add(blog);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetBlogbyid), new { id = blog.Id }, blog);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id , TblBlog blog)
        {
            var item = _context.TblBlogs.AsNoTracking().FirstOrDefault(x => x.Id == id && x.DeleteFlag == 0);
            if (item is null)
            {
                return NotFound("There is no data");
            }

            item.Title = blog.Title;
            item.Author = blog.Author;
            item.ContentData = blog.ContentData;

            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(item);
        }

        //[HttpPatch("{id}")]
        //public IActionResult PatchBlog(int id, [FromBody] JsonPatchDocument<TblBlog> patchDoc)
        //{
        //    if (patchDoc == null)
        //    {
        //        return BadRequest("Invalid patch document.");
        //    }
        //    var item = _context.TblBlogs.AsNoTracking().FirstOrDefault(x => x.Id == id && x.DeleteFlag == 0);
        //    if (item is null)
        //    {
        //        return NotFound("There is no data");
        //    }
        //    patchDoc.ApplyTo(item, ModelState);
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    _context.Entry(item).State = EntityState.Modified;
        //    _context.SaveChanges();
        //    return Ok(item);
        //}
        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var item = _context.TblBlogs.AsNoTracking().FirstOrDefault(x => x.Id == id && x.DeleteFlag == 0);
            if (item is null)
            {
                return NotFound("There is no data");
            }
            item.DeleteFlag = 1; // Soft delete
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok("Deleted successfully");
        }


    }
}
