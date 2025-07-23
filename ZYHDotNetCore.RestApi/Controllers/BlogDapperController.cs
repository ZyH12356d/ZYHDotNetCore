using Dapper;
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
        private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTraining;User ID=sa;Password=sasa@123;TrustServerCertificate=true;";

        [HttpGet]
        public IActionResult GetBlog()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Tbl_BLog WHERE Delete_flag = 0";
                var blogs = db.Query<ResponseModel>(query).ToList();
                if (blogs == null || !blogs.Any())
                {
                    return NotFound("No blogs found.");
                }
                return Ok(blogs);
            }

        }
        [HttpGet("{id}")]
        public IActionResult GetBlogById(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Tbl_BLog WHERE Id = @Id AND Delete_flag = 0";
                var blog = db.QueryFirstOrDefault<ResponseModel>(query, new { Id = id });
                if (blog == null)
                {
                    return NotFound($"Blog with ID {id} not found.");
                }
                return Ok(blog);
            }

        }
        [HttpPost]
        public IActionResult CreateBlog(RequestModel blog)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Tbl_BLog (Title, Author, Content_data, Delete_flag) VALUES (@Title, @Author, @ContentData, @DeleteFlag)";
                var result = db.Execute(query, new
                {
                    Title = blog.Title,
                    Author = blog.Author,
                    ContentData = blog.ContentData,
                    DeleteFlag = blog.DeleteFlag
                });
                if (result > 0)
                {
                    return CreatedAtAction(nameof(GetBlogById), new { id = blog.Id }, blog);
                }
                return BadRequest("Error creating the blog.");
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, ResponseModel blog)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                //string query = "UPDATE Tbl_BLog SET Title = @Title, Author = @Author, Content_data = @ContentData WHERE Id = @Id AND Delete_flag = 0";
                string query = "update Tbl_BLog set Title = @Title, Author = @Author, Content_data = @Content_data where Id = @Id";
                var result = db.Execute(query, new
                {
                    Id = id,
                    Title = blog.Title,
                    Author = blog.Author,
                    Content_data = blog.Content_Data
                });
                if (result > 0)
                {
                    return Ok("Update Successful");
                }
                return NotFound($"Blog with ID {id} not found or already deleted.");
            }

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Tbl_BLog SET Delete_flag = 1 WHERE Id = @Id";
                var result = db.Execute(query, new { Id = id });
                if (result > 0)
                {
                    return Ok("Delete Successful");
                }
                return NotFound($"Blog with ID {id} not found.");
            }
        }
    }
}
