using DoctorService.Services;
using Microsoft.AspNetCore.Mvc;
using Monitoring;
using Polly.Retry;
using SharedModels;

namespace DoctorService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorController: ControllerBase
{
    private readonly AsyncRetryPolicy _retryPolicy;
    private readonly PhysicianService _physicianService;
    public DoctorController(AsyncRetryPolicy retryPolicy)
    {
        _retryPolicy = retryPolicy;
        _physicianService = new PhysicianService(_retryPolicy);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddDoctor(DoctorIn doctorIn)
    {
        MonitorService.Log.Information("Adding doctor to database");
        return Ok(await _physicianService.AddDoctor(doctorIn));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetMeasurements(int measurementId)
    {
        MonitorService.Log.Information("Getting measurement from database");
        return Ok(await _physicianService.GetMeasurement(measurementId));
    }
    
}