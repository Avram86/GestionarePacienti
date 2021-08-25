using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionarePacienti.Models
{
    public class EditAppointmentViewModel
    {
        public int Id { get; set; }

        [DisplayName("Date")]
        [DataType(DataType.Date)]
        public DateTime DateOfAppointment { get; set; } = DateTime.Now.Date;

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


        public List<SelectListItem> Doctors { get; set; }
        public int SelectedDoctorId { get; set; }


        public List<SelectListItem> Patients { get; set; }
        public int SelectedPatientId { get; set; }

    }
}
