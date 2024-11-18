using SharedModels;

namespace DoctorService.Interfaces;

public interface IPhysicianService
{
    Task<DoctorOut> AddDoctor(DoctorIn doctorIn);
    Task<MeasurementOut> GetMeasurement(int measurementId);
}