using SharedModels;

namespace DoctorService.Interfaces;

public interface IPhysicianRepository
{
    Task<DoctorOut> AddDoctor(DoctorIn doctorIn);
    Task<MeasurementOut> GetMeasurement(int measurementId);
}