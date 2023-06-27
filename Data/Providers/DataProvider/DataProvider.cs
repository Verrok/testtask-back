using Data.Models;

namespace Data.Providers.DataProvider;

public class DataProvider: IDataProvider
{
    private readonly IDbProvider _dbProvider;
    
    public DataProvider(IDbProvider dbProvider)
    {
        _dbProvider = dbProvider;
    }
    
    public async Task<IEnumerable<ReportRow>> ProceedDataByClient(int clientId = 0, int cpId = 0)
    {
        var data = await _dbProvider.GetByClient(clientId, cpId);

        var newRows = new List<ReportRow>();
        
        foreach (var grouping in data.GroupBy(e => new { e.Name, e.DayOfWeek, e.Date }))
        {

            newRows.Add(new ReportRow()
            {
                Date = grouping.Key.Date,
                DayOfWeek = grouping.Key.DayOfWeek,
                Name = grouping.Key.Name,
                Action = string.Join(", ", grouping.Select(e => e.Action))
            });
        }

        return newRows;
    }
    
    public async Task<IEnumerable<ReportRow>> ProceedDataByCateringPoint(int cpId = 0, int clientId = 0)
    {
        var data = await _dbProvider.GetByCateringPoint(cpId, clientId);

        var newRows = new List<ReportRow>();
        
        foreach (var grouping in data.GroupBy(e => new { e.Name, e.DayOfWeek, e.Date }))
        {

            newRows.Add(new ReportRow()
            {
                Date = grouping.Key.Date,
                DayOfWeek = grouping.Key.DayOfWeek,
                Name = grouping.Key.Name,
                Action = string.Join(", ", grouping.Select(e => e.Action))
            });
        }

        return newRows;
    }
}