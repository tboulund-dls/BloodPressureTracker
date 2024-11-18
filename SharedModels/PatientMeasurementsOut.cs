namespace SharedModels;

public class PatientMeasurementsOut
{
    public string Ssn { get; set; }
    public List<MeasurementOut>? Measurements { get; set; }
}