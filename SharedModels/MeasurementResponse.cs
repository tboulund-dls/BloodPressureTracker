namespace SharedModels;

public class MeasurementResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = "";
    public MeasurementOut? Measurement { get; set; }
}