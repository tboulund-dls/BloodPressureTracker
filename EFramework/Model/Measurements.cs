using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Model;

namespace Model
{
    public class Measurements
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        public DateTime Datetime { get; set; }
        public int Systolic { get; set; }
        public int Diastolic { get; set; }
        
        [StringLength(10)]
        [ForeignKey("Patients")]
        [Required]
        public string PatientSSN { get; set; }

        public bool Seen { get; set; }

        public virtual Patients? Patient { get; set; }
    }
}