using DoctorService.Interfaces;
using Monitoring;
using Polly.Retry;
using RestSharp;
using SharedModels;

namespace DoctorService.Repositories;

public class PhysicianRepository: IPhysicianRepository
{
    private readonly RestClient _restClient;
    private readonly AsyncRetryPolicy _retryPolicy;
    
    public PhysicianRepository(AsyncRetryPolicy retryPolicy)
    {
        _retryPolicy = retryPolicy;
        _restClient = new RestClient("http://database:8080");
    }
    
    public async Task<DoctorOut> AddDoctor(DoctorIn doctorIn)
    {
        MonitorService.Log.Information("Adding doctor to database");
        var request = new RestRequest("/api/Doctor", Method.Post).AddJsonBody(doctorIn);
        var response =await _retryPolicy.ExecuteAsync(() => _restClient.PostAsync<DoctorOut>(request)
        );
        return response;
    }
    
    public async Task<MeasurementOut> GetMeasurement(int measurementId)
    {
        MonitorService.Log.Information("Getting measurement from database");
        var request = new RestRequest($"/api/Measurement/{measurementId}", Method.Get);
        var response =await _retryPolicy.ExecuteAsync(() => _restClient.GetAsync<MeasurementOut>(request)
        );
        return response;
    }
    
}