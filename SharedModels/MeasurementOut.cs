namespace SharedModels;

public class MeasurementOut
{
    public DateTime Date { get; set; }
    public int Systolic { get; set; }
    public int Diastolic { get; set; }
    public bool Seen { get; set; }
}