using DoctorService.Interfaces;
using DoctorService.Repositories;
using Monitoring;
using Polly.Retry;
using SharedModels;

namespace DoctorService.Services;

public class PhysicianService: IPhysicianService
{
    private readonly PhysicianRepository _physicianRepository;
    private readonly AsyncRetryPolicy _retryPolicy;
    
    public PhysicianService(AsyncRetryPolicy retryPolicy)
    {
        _retryPolicy = retryPolicy;
        _physicianRepository = new PhysicianRepository(_retryPolicy);
    }
    
    public async Task<DoctorOut> AddDoctor(DoctorIn doctorIn)
    {
        MonitorService.Log.Information("Adding doctor to database");
        return await _physicianRepository.AddDoctor(doctorIn);
    }
    
    public async Task<MeasurementOut> GetMeasurement(int measurementId)
    {
        MonitorService.Log.Information("Getting measurement from database");
        return await _physicianRepository.GetMeasurement(measurementId);
    }
}