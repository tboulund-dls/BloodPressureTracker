using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Monitoring;
using PatientService.Interfaces;
using PatientService.Repository;
using Polly;
using Polly.Retry;
using SharedModels;

namespace PatientService.Services;

public class PatientsService: IPatientsService
{
    private readonly AsyncRetryPolicy _retryPolicy;
    private readonly PatientRepository _patientRepository;
    
    public PatientsService()
    {
        _patientRepository = new PatientRepository(_retryPolicy);
    }
    
    public PatientsService(AsyncRetryPolicy retryPolicy): this()
    {
        _retryPolicy = retryPolicy;
    }

    public async Task<PatientOut> AddPatient(PatientIn patientIn)
    {
        MonitorService.Log.Information("Adding patient to database");
        return await _patientRepository.AddPatient(patientIn);
    }
    
    public async Task<MeasurementOut> AddMeasurement(MeasurementIn measurementIn)
    {
        MonitorService.Log.Information("Adding measurement to database");
        return await _patientRepository.AddMeasurement(measurementIn);
    }
}