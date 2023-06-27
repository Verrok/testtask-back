using Data.Models;

namespace Data.Providers;

public interface IDbProvider: ITransientProvider
{
    Task<List<ReportRow>> GetByClient(int clientId, int cpId = 0);
    Task<List<ReportRow>> GetByCateringPoint(int cpId, int clientId = 0);
    Task<IEnumerable<ListRow>> GetClients();
    Task<ListRow> GetClient(int clientId);
    Task<ListRow> GetCateringPoint(int cpId);
    Task<IEnumerable<ListRow>> GetCateringPoints();
}