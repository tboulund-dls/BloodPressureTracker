using MeasureService.Interfaces;
using Monitoring;
using Polly.Retry;
using RestSharp;
using SharedModels;

namespace MeasureService.Repositories;

public class MeasureRepository: IMeasureRepository
{
    private readonly AsyncRetryPolicy _retryPolicy;
    private readonly RestClient _restClient;
    
    public MeasureRepository(AsyncRetryPolicy retryPolicy)
    {
        _retryPolicy = retryPolicy;
        _restClient = new RestClient("http://database:8080");
    }

    public async Task<MeasurementResponse> AddMeasurement(MeasurementIn measurementIn)
    {
        MonitorService.Log.Information("Adding measurement to database");
        var request = new RestRequest("/api/Measurement", Method.Post).AddJsonBody(measurementIn);
        var apiresponse =await _retryPolicy.ExecuteAsync(() => _restClient.PostAsync<MeasurementOut>(request)
        );
        return new MeasurementResponse
        {
            Success = apiresponse != null ? true : false,
            Message = apiresponse != null
                ? "Everything is fine"
                : "It seems like we can't get rid of data. Try again later",
            Measurement = apiresponse
        };
    }

    public async Task<MeasurementResponse> GetMeasurement(int measurementId)
    {
        MonitorService.Log.Information("Getting measurement from database");
        var request = new RestRequest($"/api/Measurement/{measurementId}", Method.Get);
        var response =await _retryPolicy.ExecuteAsync(() => _restClient.GetAsync<MeasurementOut>(request)
        );
        return new MeasurementResponse
        {
            Success = response != null ? true : false,
            Message = response != null
                ? "Everything is fine"
                : "It seems like we can't read data right now. Try again later",
        };
    }
}