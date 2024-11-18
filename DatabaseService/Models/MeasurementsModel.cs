using Microsoft.EntityFrameworkCore;

namespace DatabaseService.Models;

[PrimaryKey(nameof(MeasurementId))]
public class MeasurementsModel
{
    public int MeasurementId { get; set; }
    public DateTime Date { get; set; }
    public int Systolic { get; set; }
    public int Diastolic { get; set; }
    public DoctorModel? Seen { get; set; }
    public PatientModel Patient { get; set; }
}