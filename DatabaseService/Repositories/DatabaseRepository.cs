using DatabaseService.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Retry;
using SharedModels;

namespace DatabaseService.Repositories;

public class DatabaseRepository
{
    private readonly MeasurementsContext _context;
    private readonly AsyncPolicy _retryInsertPolicy;
    
    public DatabaseRepository(MeasurementsContext context, AsyncRetryPolicy retryPolicy)
    {
        _context = context;
        _retryInsertPolicy = retryPolicy;
    }

    public async Task<ActionResult<MeasurementIn>> AddMeasurement(MeasurementIn measurementIn)
    {
        var patient = await _context.Patients.FirstOrDefaultAsync(x => x.Ssn == measurementIn.Ssn);
        
        patient.Measurements ??= new List<MeasurementsModel>();
          
        var measurement = new MeasurementsModel{
            Date = DateTime.Now,
            Systolic = measurementIn.Systolic,
            Diastolic = measurementIn.Diastolic,
            Patient = patient
        };
            
        patient.Measurements.Add(measurement);
        
        await _retryInsertPolicy.ExecuteAsync(async () => await _context.SaveChangesAsync());
        _context.Patients.Update(patient);
        
        return measurementIn;
    }

    public async Task<ActionResult<PatientIn>> AddPatient(PatientIn patientIn)
    {
        var patient = new PatientModel
        {
            Ssn = patientIn.SSN,
            Name = patientIn.Name,
            Email = patientIn.Email,
        };
        
        await _retryInsertPolicy.ExecuteAsync(async () => await _context.Patients.AddAsync(patient));
        await _context.SaveChangesAsync();
        
        return patientIn;
    }

    public async Task<ActionResult<List<MeasurementOut>>> GetMeasurementsSeenByDoctor()
    {
        var measurements = await _context.Measurements
            .Where(x => x.Seen != null)
            .Select(x => new MeasurementOut
            {
                // Map the properties from MeasurementModel to MeasurementOut
                Date = x.Date,
                Systolic = x.Systolic,
                Diastolic = x.Diastolic,
                Seen = x.Seen != null
            })
            .ToListAsync();
        return measurements;
    }
}