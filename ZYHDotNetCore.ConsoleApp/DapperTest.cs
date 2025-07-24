using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZYHDotNetCore.ConsoleApp.Model;
using ZYHDotNetCore.Share;

namespace ZYHDotNetCore.ConsoleApp
{
    public class DapperTest
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTraining;User ID=sa;Password=sasa@123;TrustServerCertificate=true;";
        private readonly DapperService _dapperService;
        public DapperTest()
        {
            _dapperService = new DapperService(_connectionString);
        }
        public void Read()
        {
            string query = "select * from Tbl_BLog where Delete_flag = 0";
            var list = _dapperService.Query<BlogModel>(query).ToList();
            foreach (var item in list)
            {
                Console.WriteLine(item.Id);
                Console.WriteLine(item.Title);
                Console.WriteLine(item.Author);
                Console.WriteLine(item.Content_data);
                Console.WriteLine("--------------------");
            }
        }

        public void GetById()
        {
            Console.Write("Enter Id :");
            string id = Console.ReadLine();

            string query = "select * from Tbl_BLog where Id = @Id and Delete_flag = 0";
            var item = _dapperService.QueryFirstOrDefault<BlogModel>(query, new { Id = id });
            if (item != null)
            {
                Console.WriteLine(item.Id);
                Console.WriteLine(item.Title);
                Console.WriteLine(item.Author);
                Console.WriteLine(item.Content_data);
            }
            else
            {
                Console.WriteLine("No record found with the given Id.");
            }
        }

        public void Create(string title, string author, string content)
        {
            string query = "insert into Tbl_BLog (Title, Author, Content_data , Delete_flag) values (@Title, @Author, @Content_data , @DeleteFlag)";
            var result = _dapperService.Excute(query, new 
            {
                Title = title
               ,
                Author = author
               ,
                Content_data = content
               ,
                DeleteFlag = 0
            });
            Console.WriteLine($"{result} row(s) inserted.");
            

        }
    }
}
