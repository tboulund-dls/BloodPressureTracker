using MeasureService.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Monitoring;
using Polly.Retry;
using SharedModels;

namespace MeasureService.Controllers;


[ApiController]
[Route("api/[controller]")]
public class MeasureController: ControllerBase
{
    private readonly AsyncRetryPolicy _retryPolicy;
    private readonly MeasureRepository _measureRepository;
    
    public MeasureController(AsyncRetryPolicy retryPolicy)
    {
        _retryPolicy = retryPolicy;
        _measureRepository = new MeasureRepository(_retryPolicy);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddMeasurement(MeasurementIn measurementIn)
    {
        MonitorService.Log.Information("Adding measurement to database");
        return Ok(await _measureRepository.AddMeasurement(measurementIn));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetMeasurements(int measurementId)
    {
        MonitorService.Log.Information("Getting measurement from database");
        return Ok(await _measureRepository.GetMeasurement(measurementId));
    }
    
}