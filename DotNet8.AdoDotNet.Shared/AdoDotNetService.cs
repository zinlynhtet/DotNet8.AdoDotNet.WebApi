using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace DotNet8.AdoDotNet.Shared;

public  class AdoDotNetService
{
    private readonly string _connectionString;

    public AdoDotNetService(string connectionString) => _connectionString = connectionString;

    public List<T> Query<T>(string query, params AdoDotNetParameter[]? parameters)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        SqlCommand command = new SqlCommand(query, connection);
        if (parameters is not null && parameters.Length > 0)
        {
            var array = parameters.Select(x => new SqlParameter(x.Name, x.Value)).ToArray();
            command.Parameters.AddRange(array);
        }

        SqlDataAdapter adapter = new SqlDataAdapter(command);
        DataTable dt = new DataTable();
        adapter.Fill(dt);
        connection.Close();
        string json = JsonConvert.SerializeObject(dt);
        var lst = JsonConvert.DeserializeObject<List<T>>(json);
        return lst;
    }

    public T QueryFirstOrDefault<T>(string query, params AdoDotNetParameter[]? parameters)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        SqlCommand command = new SqlCommand(query, connection);
        if (parameters is not null && parameters.Length > 0)
        {
            var array = parameters.Select(x => new SqlParameter(x.Name, x.Value)).ToArray();
            command.Parameters.AddRange(array);
        }

        SqlDataAdapter adapter = new SqlDataAdapter(command);
        DataTable dt = new DataTable();
        adapter.Fill(dt);
        connection.Close();
        string json = JsonConvert.SerializeObject(dt);
        var lst = JsonConvert.DeserializeObject<List<T>>(json);
        return lst[0];
    }
    public int Execute(string query, params AdoDotNetParameter[]? parameters)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        SqlCommand cmd = new SqlCommand(query, connection);
        if (parameters is not null && parameters.Length > 0)
        {
            cmd.Parameters.AddRange(parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray());
        }

        var result = cmd.ExecuteNonQuery();

        connection.Close();
        return result;
    }

    public abstract class AdoDotNetParameter
    {
        private AdoDotNetParameter()
        {
        }

        private AdoDotNetParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public object Value { get; set; }
    }
}