using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using ZYHDotNetCore.Database.AppDbContextModels;
using ZYHDotNetCore.RestApi.Models.BlogModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ZYHDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTraining;User ID=sa;Password=sasa@123;TrustServerCertificate=true;";

        AppDbContext _context = new AppDbContext();
        [HttpGet]
        public IActionResult GetBlog()
        {
            List<ResponseModel> responseModel = new List<ResponseModel>();
            SqlConnection connecton = new SqlConnection(_connectionString);
            string query = "Select * from Tbl_BLog where Delete_flag = 0";
            connecton.Open();
            Console.WriteLine("Connection Success");
            SqlCommand cmd = new SqlCommand(query, connecton);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["Id"]);
                Console.WriteLine(reader["Title"]);
                Console.WriteLine(reader["Author"]);
                Console.WriteLine(reader["Content_data"]);
                Console.WriteLine("--------------------");
                var model = new ResponseModel
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Title = reader["Title"].ToString(),
                        Author = reader["Author"].ToString(),
                        ContentData = reader["Content_data"].ToString()
                    };
                responseModel.Add(model);
            }
            connecton.Close();
            return Ok(responseModel);

        }

        [HttpGet("{id}")]
        public IActionResult GetBlogById(int id)
        {
            ResponseModel blog = new ResponseModel();
            SqlConnection connecton = new SqlConnection(_connectionString);
            string query = "Select * from Tbl_BLog where Delete_flag = 0 and Id = @Id";
            connecton.Open();
            Console.WriteLine("Connection Success");
            SqlCommand cmd = new SqlCommand(query, connecton);
            cmd.Parameters.AddWithValue("@Id", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                blog.Id = Convert.ToInt32( row["Id"] );
                blog.Title = row["Title"].ToString();
                blog.Author = row["Author"].ToString();
                blog.ContentData = row["Content_data"].ToString();
            }
            return Ok(blog);
            
        }


        [HttpPost]
        public IActionResult CreateBlog(RequestModel blog)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = $@"INSERT INTO [dbo].[Tbl_Blog]
                               ([Title]
                               ,[Author]
                               ,[Content_data]
                               ,[Delete_flag])
                         VALUES
                               (@title
                               ,@author
                               ,@content
                               ,0)";
            
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@title", blog.Title);
            cmd.Parameters.AddWithValue("@author", blog.Author);
            cmd.Parameters.AddWithValue("@content", blog.ContentData);
            Console.WriteLine("Incert success");
            
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            

            return CreatedAtAction(nameof(GetBlogById), new { id = blog.Id }, blog);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, ResponseModel blog)
        {

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string updateQuery = $@"UPDATE Tbl_Blog
                                        SET Title = @newTitle, Author = @newAuthor, Content_data = @newContent
                                        WHERE Id = @Id";
            SqlCommand updateCmd = new SqlCommand(updateQuery, connection);
            updateCmd.Parameters.AddWithValue("@Id", id);
            updateCmd.Parameters.AddWithValue("@newTitle", blog.Title);
            updateCmd.Parameters.AddWithValue("@newAuthor", blog.Author);
            updateCmd.Parameters.AddWithValue("@newContent", blog.ContentData);
            int rowsAffected = updateCmd.ExecuteNonQuery();
            connection.Close();

            if (rowsAffected > 0)
            {
                return Ok("Record update successfully.");
            }
            else
            {
                return NotFound("Record not found.");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string deleteQuery = $@"UPDATE Tbl_Blog
                                        SET Delete_flag = 1
                                        WHERE Id = @Id";
            SqlCommand deleteCmd = new SqlCommand(deleteQuery, connection);
            deleteCmd.Parameters.AddWithValue("@Id", id);
            int rowsAffected = deleteCmd.ExecuteNonQuery();
            connection.Close();
            if (rowsAffected > 0)
            {
                return Ok("Record deleted successfully.");
            }
            else
            {
                return NotFound("Record not found.");
            }
        }
    }
}
