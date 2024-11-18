using SharedModels;
using Monitoring;
using Polly;
using Polly.Retry;
using RestSharp;
using Serilog;

namespace PatientService.Repository;

public class PatientRepository
{
    private readonly RestClient _restClient;
    private readonly AsyncRetryPolicy _retryPolicy;
    public PatientRepository(AsyncRetryPolicy retryPolicy)
    {
        _retryPolicy = retryPolicy;
        _restClient = new RestClient("http://database:8080");
    }

    public async Task<PatientOut> AddPatient(PatientIn patientIn)
    {
        MonitorService.Log.Information("Adding patient to database");
        var request = new RestRequest("/api/Patient", Method.Post).AddJsonBody(patientIn);
        var _response =await _retryPolicy.ExecuteAsync(() => _restClient.PostAsync<PatientOut>(request)
        );
        return _response;
    }
    
    public async Task<MeasurementOut> AddMeasurement(MeasurementIn measurementIn)
    {
        MonitorService.Log.Information("Adding measurement to database");
        var request = new RestRequest("/api/Patient/Measurements", Method.Post).AddJsonBody(measurementIn);
        var _response =await _retryPolicy.ExecuteAsync(() => _restClient.PostAsync<MeasurementOut>(request)
        );
        return _response;
    }
    
}