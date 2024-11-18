using System.Collections.ObjectModel;
using System.Diagnostics;
using SharedModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DatabaseService.Models;
using DatabaseService.Repositories;
using Monitoring;
using MySql.Data.MySqlClient;
using Polly;
using Polly.Retry;
using SharedModels;

namespace DatabaseService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController: ControllerBase
    {
        private readonly MeasurementsContext _context;
        private readonly AsyncRetryPolicy _retryInsertPolicy;
        private readonly DatabaseRepository _databaseRepository;

        public PatientController(MeasurementsContext context)
        {
            _context = context;
            _retryInsertPolicy = Policy.Handle<MySqlException>().WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
            _databaseRepository = new DatabaseRepository(_context, _retryInsertPolicy);
        }

        [HttpPost]
        public async Task<ActionResult<PatientIn>> Post(PatientIn patientIn)
        {
            return await _databaseRepository.AddPatient(patientIn);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<MeasurementIn>> Post(MeasurementIn measurementIn)
        {
            MonitorService.Log.Information("Adding measurement to database");
            var response = _databaseRepository.AddMeasurement(measurementIn);
            
            return Ok(response);
        }
    
    }
}

