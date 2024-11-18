using Microsoft.EntityFrameworkCore;

namespace DatabaseService.Models;

public class MeasurementsContext: DbContext
{
    public DbSet<DoctorModel> Doctors { get; set; }
    public DbSet<PatientModel> Patients { get; set; }
    public DbSet<MeasurementsModel> Measurements { get; set; }
    
    public MeasurementsContext(DbContextOptions<MeasurementsContext> options) : base(options)
    {
    }
}