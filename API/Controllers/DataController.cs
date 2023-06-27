using Data.Providers;
using Data.Providers.DataProvider;
using Microsoft.AspNetCore.Mvc;

namespace TestTask2.Controllers;

[ApiController]
[Route("[controller]")]
public class DataController: ControllerBase
{
    private readonly IDataProvider _dataProvider;
    private readonly IDbProvider _dbProvider;

    public DataController(IDataProvider dataProvider, IDbProvider dbProvider)
    {
        _dataProvider = dataProvider;
        _dbProvider = dbProvider;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> AllClients()
    {
        var data = await _dbProvider.GetClients();

        return new JsonResult(data);
    }
    
    [HttpGet("[action]")]
    public async Task<IActionResult> AllCateringPoints()
    {
        var data = await _dbProvider.GetCateringPoints();
        return new JsonResult(data);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetClientData([FromQuery] int clientId)
    {
        var data = await _dbProvider.GetClient(clientId);
        return new JsonResult(data);
    }
    
    [HttpGet("[action]")]
    public async Task<IActionResult> GetCateringPointData([FromQuery] int cpId)
    {
        var data = await _dbProvider.GetCateringPoint(cpId);
        return new JsonResult(data);
    }
    
    [HttpGet("[action]")]
    public async Task<IActionResult> Client([FromQuery] int clientId, [FromQuery] int cpId)
    {

        var data = await _dataProvider.ProceedDataByClient(clientId, cpId);
        return new JsonResult(data);
    }
    
    [HttpGet("[action]")]
    public async Task<IActionResult> CateringPoint([FromQuery] int clientId, [FromQuery] int cpId)
    {

        var data = await _dataProvider.ProceedDataByCateringPoint(cpId, clientId);
        return new JsonResult(data);
    }
}