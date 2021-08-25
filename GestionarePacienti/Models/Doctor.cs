using GestionarePacienti.Enumerations;
using GestionarePacienti.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestionarePacienti.Models
{
    public class Doctor:BaseClass
    {

        [Required(ErrorMessage = "This is a required field")]
        [RegularExpression("^[a-zA-Z ]+$")]
        [MaxLength(150, ErrorMessage = "The field must be under 150 characters!")]
        public string Name { get; set; }= string.Empty;


        public Specialization Specialization{ get; set; }

        public List<AppointmentDetails> Appointments { get; set; }
    }
}
