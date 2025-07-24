using System;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ZYHDotNetCore.Share
{
    public class DapperService
    {
        private readonly string _connectionString;
        public DapperService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<T> Query<T>(string query , object? param = null)
        {
            using ( IDbConnection db = new SqlConnection(_connectionString))
            {
                var lst = db.Query<T>(query , param).ToList();
                return lst;
            }
        }
        public T QueryFirstOrDefault<T>(string query , object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);            
            var item = db.QueryFirstOrDefault<T>(query, param);
            return item;
            
        }
        public int Excute(string query, object? param = null)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var item = db.Execute(query, param);
                return item;
            } 
        }
    }
}
