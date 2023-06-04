using Microsoft.Data.SqlClient;
using System.Data;

namespace PI6.WebApi.Helpers;

public class SqlParam
{
    private readonly List<SqlParameter> _params;

    public SqlParam()
    {
        _params = new();
    }

    public void AddParam(string name, object? value, SqlDbType type)
    {
        _params.Add(new SqlParameter()
        {
            ParameterName = name,
            Value = value ?? DBNull.Value,
            SqlDbType = type
        });
    }

    public void ClearParams() => _params.Clear();

    public object[] Params() => _params.ToArray();
}