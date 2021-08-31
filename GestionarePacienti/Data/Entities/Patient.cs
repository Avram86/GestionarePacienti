using GestionarePacienti.Enumerations;
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
    public class Patient:BaseClass
    {

        [Required(ErrorMessage = "This is a required field")]
        [RegularExpression("^[a-zA-Z ]+$")]
        [MaxLength(150, ErrorMessage = "The Name field must be under 150 characters!")]
        public string Name { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [DisplayName("Date of birth")]
        public DateTime? DateOfBirth { get; set; } = null;

        public Gender Gender { get; set; }

        [DisplayName("Marital status")]
        public MaritalStatus MaritalStatus { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        [RegularExpression("^[a-zA-Z]+$")]
        [MaxLength(150, ErrorMessage = "The field must be under 150 characters!")]
        public string County { get; set; } = string.Empty;

        [Required(ErrorMessage = "This is a required field")]
        [MaxLength(150, ErrorMessage = "The field must be under 150 characters!")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "This is a required field")]
        [MaxLength(150, ErrorMessage = "The field must be under 150 characters!")]
        public string Address { get; set; } = string.Empty;


        [Required(ErrorMessage = "This is a required field")]
        [RegularExpression("^[0-9]*$",ErrorMessage ="Digits only!")]
        public string Phone { get; set; } = string.Empty;

        public List<AppointmentDetails> Appointments { get; set; }

    }
}
