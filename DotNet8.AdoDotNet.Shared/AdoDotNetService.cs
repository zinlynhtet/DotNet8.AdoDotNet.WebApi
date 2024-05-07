using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace DotNet8.AdoDotNet.Shared;

public class AdoDotNetService
{
    private readonly string _connectionString;

    public AdoDotNetService(string connectionString) => _connectionString = connectionString;

    public async Task<List<T>> Query<T>(string query, params AdoDotNetParameter[]? parameters)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        SqlCommand command = new SqlCommand(query, connection);
        if (parameters is not null && parameters.Length > 0)
        {
            var array = parameters.Select(x => new SqlParameter(x.Name, x.Value)).ToArray();
            command.Parameters.AddRange(array);
        }

        SqlDataAdapter adapter = new SqlDataAdapter(command);
        DataTable dt = new DataTable();
        await Task.Run(() => adapter.Fill(dt));
        await connection.CloseAsync();
        string json = JsonConvert.SerializeObject(dt);
        var lst = JsonConvert.DeserializeObject<List<T>>(json);
        return lst;
    }

    public async Task<T> QueryFirstOrDefault<T>(string query, params AdoDotNetParameter[]? parameters)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        SqlCommand command = new SqlCommand(query, connection);
        if (parameters is not null && parameters.Length > 0)
        {
            var array = parameters.Select(x => new SqlParameter(x.Name, x.Value)).ToArray();
            command.Parameters.AddRange(array);
        }

        SqlDataAdapter adapter = new SqlDataAdapter(command);
        DataTable dt = new DataTable();
        await Task.Run(() => adapter.Fill(dt));
        await connection.CloseAsync();

        string json = JsonConvert.SerializeObject(dt);
        var lst = JsonConvert.DeserializeObject<List<T>>(json);
        return lst[0];
    }

    public async Task<int> Execute(string query, params AdoDotNetParameter[]? parameters)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        SqlCommand cmd = new SqlCommand(query, connection);
        if (parameters is not null && parameters.Length > 0)
        {
            cmd.Parameters.AddRange(parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray());
        }

        var result = cmd.ExecuteNonQuery();

        await connection.CloseAsync();
        return result;
    }

   
}