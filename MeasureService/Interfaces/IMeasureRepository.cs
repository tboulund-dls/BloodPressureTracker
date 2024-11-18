using SharedModels;

namespace MeasureService.Interfaces;

public interface IMeasureRepository
{
    Task<MeasurementResponse> AddMeasurement(MeasurementIn measurementIn);
    Task<MeasurementResponse> GetMeasurement(int measurementId);
}