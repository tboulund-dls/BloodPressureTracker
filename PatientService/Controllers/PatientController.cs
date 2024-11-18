using Microsoft.AspNetCore.Mvc;
using Polly.Retry;
using SharedModels;
using Monitoring;
using PatientService.Interfaces;


namespace PatientService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController(IPatientsService patientsService, AsyncRetryPolicy retryPolicy): ControllerBase
{
    private readonly IPatientsService _patientService = patientsService;
    private readonly AsyncRetryPolicy _retryPolicy = retryPolicy;
    
    
    [HttpPost]
    public async Task<IActionResult> AddPatient(PatientIn patientIn)
    {
         MonitorService.Log.Information("Adding patient to database");
    var patientOut = await _patientService.AddPatient(patientIn);
    return Ok(patientOut);
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> AddMeasurement(MeasurementIn measurementIn)
    {
        MonitorService.Log.Information("Adding measurement to database");
        var measurementOut = await _patientService.AddMeasurement(measurementIn);
        return Ok(measurementOut);
    }
}