using System.Data;
using Dapper;
using Data.Models;
using Microsoft.Extensions.Options;
using Npgsql;
using TestTask2.Options;

namespace Data.Providers;

public class DbProvider: IDbProvider
{
    private readonly DbOptions _options;
    
    public DbProvider(IOptions<DbOptions> dbOptions)
    {
        _options = dbOptions.Value;
    }
    
    public async Task<List<ReportRow>> GetByClient(int clientId, int cpId = 0)
    {
        await using var conn = new NpgsqlConnection(_options.ConnectionString);

        var p = new DynamicParameters();

        p.Add("@clientId", clientId, DbType.Int32);
        p.Add("@cpId", cpId, DbType.Int32);
        
        var res = await conn.QueryAsync<ReportRow>(
            """
           select * from get_by_client(@clientId, @cpId)
        """, p);

        return res.ToList();
    }

    public async Task<List<ReportRow>> GetByCateringPoint(int cpId, int clientId = 0)
    {
        await using var conn = new NpgsqlConnection(_options.ConnectionString);

        var p = new DynamicParameters();

        p.Add("@clientId", clientId, DbType.Int32);
        p.Add("@cpId", cpId, DbType.Int32);
        
        var res = await conn.QueryAsync<ReportRow>(
            """
           select * from get_by_cp(@cpId, @clientId)
        """, p);

        return res.ToList();
    }
    
    public async Task<IEnumerable<ListRow>> GetClients()
    {
        await using var conn = new NpgsqlConnection(_options.ConnectionString);

        var res = await conn.QueryAsync<ListRow>(
            """select * from "Client" """);

        return res.ToList();
    }
    
    public async Task<ListRow> GetClient(int clientId)
    {
        await using var conn = new NpgsqlConnection(_options.ConnectionString);

        var p = new DynamicParameters();

        p.Add("@id", clientId, DbType.Int32);
        
        var res = await conn.QueryFirstOrDefaultAsync<ListRow>(
            """
                select * from "Client"
                where "Id" = @id
                """, p);

        return res;
    }
    
    public async Task<ListRow> GetCateringPoint(int cpId)
    {
        await using var conn = new NpgsqlConnection(_options.ConnectionString);

        var p = new DynamicParameters();

        p.Add("@id", cpId, DbType.Int32);
        
        var res = await conn.QueryFirstOrDefaultAsync<ListRow>(
            """
                select * from "CateringPoint"
                where "Id" = @id
                """, p);

        return res;
    }
    
    public async Task<IEnumerable<ListRow>> GetCateringPoints()
    {
        await using var conn = new NpgsqlConnection(_options.ConnectionString);

        var res = await conn.QueryAsync<ListRow>(
            """select * from "CateringPoint" """);

        return res.ToList();
    }
}