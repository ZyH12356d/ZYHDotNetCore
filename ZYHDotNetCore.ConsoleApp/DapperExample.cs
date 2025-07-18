using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZYHDotNetCore.ConsoleApp.Model;

namespace ZYHDotNetCore.ConsoleApp
{

    public class DapperExample
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTraining;User ID=sa;Password=sasa@123;";

        public void Read()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "select * from Tbl_BLog where Delete_flag = 0";
                var list = db.Query<BlogModel>(query).ToList();
                foreach (var item in list)
                {
                    Console.WriteLine(item.Id);
                    Console.WriteLine(item.Title);
                    Console.WriteLine(item.Author);
                    Console.WriteLine(item.Content_data);
                    Console.WriteLine("--------------------");
                }
            }
        }
            public void Create(string title, string author, string content)
            {
            
            using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    string query = "insert into Tbl_BLog (Title, Author, Content_data , Delete_flag) values (@Title, @Author, @Content_data , @DeleteFlag)";
                var result = db.Execute(query, new {
                    Title = title
                   , Author = author
                   , Content_data = content
                   , DeleteFlag = 0
                });
                    Console.WriteLine($"{result} row(s) inserted.");
                }

            }
        public void Update(int id , string title, string author, string content)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "update Tbl_BLog set Title = @Title, Author = @Author, Content_data = @Content_data where Id = @Id";
                var result = db.Execute(query, new
                {
                    Id = id,
                    Title = title,
                    Author = author,
                    Content_data = content
                });
                Console.WriteLine($"{result} row(s) updated.");
            }
        }
        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "update Tbl_BLog set Delete_flag = 1 where Id = @Id";
                var result = db.Execute(query, new { Id = id });
                Console.WriteLine($"{result} row(s) deleted.");
            }
        }
    }

}
