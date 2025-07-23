using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZYHDotNetCore.Share
{
    public class AdoDotNetService
    {
        private readonly string _connectionString;
        public AdoDotNetService(string connectionString)
        {
            _connectionString = connectionString;
        }
        public DataTable Query(string query , params SqlParameter[] sqlParameter)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query ,connection);
            if (sqlParameter is not null)
            {
                foreach (var param in sqlParameter)
                {
                    cmd.Parameters.AddWithValue(param.ParameterName, param.Value);
                }
            }
            
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();
            return dt;
        }
    }

    public class SqlParameter
    {
        public string ParameterName { get; set; }
        public object Value { get; set; }

        public SqlParameter(string parameterName, object value)
        {
            ParameterName = parameterName;
            Value = value;
        }
    }
}
