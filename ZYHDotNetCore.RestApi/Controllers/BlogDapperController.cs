using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using ZYHDotNetCore.RestApi.Models.BlogModel;

namespace ZYHDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTraining;User ID=sa;Password=sasa@123;";

        [HttpGet]
        public IActionResult GetBlog()
        {
            return NoContent();

        }
        [HttpGet("{id}")]
        public IActionResult GetBlogById(int id)
        {
            return NoContent();

        }
        [HttpPost]
        public IActionResult CreateBlog(RequestModel blog)
        {
            return NoContent();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, ResponseModel blog)
        {
            return NoContent();

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            return NoContent();
        }
    }
}
