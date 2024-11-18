using Microsoft.AspNetCore.Mvc;
using SharedModels;

namespace PatientService.Interfaces;

public interface IPatientsService
{
    Task<PatientOut> AddPatient(PatientIn patientIn);
    Task<MeasurementOut> AddMeasurement(MeasurementIn measurementIn);
}