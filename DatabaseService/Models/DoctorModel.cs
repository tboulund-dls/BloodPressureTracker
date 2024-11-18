using Microsoft.EntityFrameworkCore;

namespace DatabaseService.Models;

[PrimaryKey(nameof(DoctorId))]
public class DoctorModel
{
    public int DoctorId { get; set; }
    public string Name { get; set; }
}