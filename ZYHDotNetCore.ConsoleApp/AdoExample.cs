using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZYHDotNetCore.Share;

namespace ZYHDotNetCore.ConsoleApp
{
    public class AdoExample
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTraining;User ID=sa;Password=sasa@123;TrustServerCertificate=true;";

        private readonly AdoDotNetService _adoDotNetService;

        public AdoExample()
        {
            _adoDotNetService = new AdoDotNetService(_connectionString);
        }

        public void Read()
        {
            string query = "Select * from Tbl_BLog where Delete_flag = 0";
            var dt = _adoDotNetService.Query(query);
            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine(row["Id"]);
                Console.WriteLine(row["Title"]);
                Console.WriteLine(row["Author"]);
                Console.WriteLine(row["Content_data"]);

            }

        }
        public void Edit()
        {
            Console.Write("Enter Id to Edit : ");
            string id = Console.ReadLine();

            string query = $"SELECT * FROM Tbl_Blog WHERE Id = @Id";

            var dt = _adoDotNetService.Query(query, new SqlParameter("@Id" , id));
            DataRow row = dt.Rows[0];
            Console.WriteLine("Current Title: " + row["Title"]);
            Console.WriteLine("Current Author: " + row["Author"]);
            Console.WriteLine("Current Content: " + row["Content_data"]);
        }
    }
}
