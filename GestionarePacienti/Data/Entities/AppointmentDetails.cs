using GestionarePacienti.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestionarePacienti.Data.Entities
{
    public class AppointmentDetails:BaseClass
    {
  
        [DisplayName("Date")]
        [DataType(DataType.Date)]
        public DateTime DateOfAppointment { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "This is a required field")]
        [MaxLength(250, ErrorMessage = "The field must be under 250 characters!")]
        public string Symptoms { get; set; } = string.Empty;

        [Required(ErrorMessage = "This is a required field")]
        [MaxLength(250, ErrorMessage = "The field must be under 250 characters!")]
        public string Diagnostic { get; set; } = string.Empty;

        [Required(ErrorMessage = "This is a required field")]
        [MaxLength(100, ErrorMessage = "The field must be under 100 characters!")]
        [DisplayName("Diagnostic code")]
        public string DiagnosticCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "This is a required field")]
        [MaxLength(250, ErrorMessage = "The field must be under 250 characters!")]
        public string Recommandation { get; set; } = string.Empty;



        //navigation properties
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }

        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; }

    }
}
