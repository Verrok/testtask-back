using Data.Models;

namespace Data.Providers.DataProvider;

public interface IDataProvider: ITransientProvider
{
    Task<IEnumerable<ReportRow>> ProceedDataByClient(int clientId = 0, int cpId = 0);
    Task<IEnumerable<ReportRow>> ProceedDataByCateringPoint(int cpId = 0, int clientId = 0);
}