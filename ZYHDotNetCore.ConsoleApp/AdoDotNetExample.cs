using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZYHDotNetCore.ConsoleApp
{
    public class AdoDotNetExample
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTraining;User ID=sa;Password=sasa@123;";
        public void read()
        {
            SqlConnection connecton = new SqlConnection(_connectionString);
            string query = "Select * from Tbl_BLog where Delete_flag = 0";
            connecton.Open();
            Console.WriteLine("Connection Success");
            SqlCommand cmd = new SqlCommand(query, connecton);
            //SqlDataReader reader = cmd.ExecuteReader();
            //while (reader.Read())
            //{
            //    Console.WriteLine(reader["Id"]);
            //    Console.WriteLine(reader["Title"]);
            //    Console.WriteLine(reader["Author"]);
            //    Console.WriteLine(reader["Content_data"]);
            //    Console.WriteLine("--------------------");
            //}
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            connecton.Close();
            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine(row["Id"]);
                Console.WriteLine(row["Title"]);
                Console.WriteLine(row["Author"]);
                Console.WriteLine(row["Content_data"]);

            }
        }
        public int Create()
        {
            Console.Write("Enter Title : ");
            string title = Console.ReadLine();
            Console.Write("Enter Author : ");
            string author = Console.ReadLine();
            Console.Write("Enter Content : ");
            string content = Console.ReadLine();
            Console.Write("Enter Id : ");
            string id = Console.ReadLine();
            // Insert data into Tbl_Blog
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
            //string query2 = $"select * from Tbl_Blog where Id = @Id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@author", author);
            cmd.Parameters.AddWithValue("@content", content);
            Console.WriteLine("Incert success");
            //SqlDataAdapter da = new SqlDataAdapter(cmd2);
            //DataTable datatable = new DataTable();
            //da.Fill(datatable);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            //foreach (DataRow row in datatable.Rows)
            //{
            //    Console.WriteLine(row["Id"]);
            //    Console.WriteLine(row["Title"]);
            //    Console.WriteLine(row["Author"]);
            //    Console.WriteLine(row["Content_data"]);
            //}

            return result;
        }
        public void Edit()
        {
            Console.Write("Enter Id to Edit : ");
            string id = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = $"SELECT * FROM Tbl_Blog WHERE Id = @Id";
            SqlCommand cmd = new SqlCommand(query , connection);
            cmd.Parameters.AddWithValue("@Id", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                Console.WriteLine("Current Title: " + row["Title"]);
                Console.Write("Enter New Title: ");
                string newTitle = Console.ReadLine();
                Console.WriteLine("Current Author: " + row["Author"]);
                Console.Write("Enter New Author: ");
                string newAuthor = Console.ReadLine();
                Console.WriteLine("Current Content: " + row["Content_data"]);
                Console.Write("Enter New Content: ");
                string newContent = Console.ReadLine();
                string updateQuery = $@"UPDATE Tbl_Blog
                                        SET Title = @newTitle, Author = @newAuthor, Content_data = @newContent
                                        WHERE Id = @Id";
                SqlCommand updateCmd = new SqlCommand(updateQuery, connection);
                updateCmd.Parameters.AddWithValue("@Id", id);
                updateCmd.Parameters.AddWithValue("@newTitle", newTitle);
                updateCmd.Parameters.AddWithValue("@newAuthor", newAuthor);
                updateCmd.Parameters.AddWithValue("@newContent", newContent);
                int rowsAffected = updateCmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Record updated successfully.");
                }
            }
            else
            {
                Console.WriteLine("No record found with the given Id.");
            }
            connection.Close();
        }
        public void Delete()
        {
            Console.Write("Enter Id to Delete : ");
            string id = Console.ReadLine();
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = $"Update Tbl_Blog set Delete_flag = 1 WHERE Id = @Id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", id);
            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Console.WriteLine("Record deleted successfully.");
            }
            else
            {
                Console.WriteLine("No record found with the given Id.");
            }
            connection.Close();
        }
    }
}
