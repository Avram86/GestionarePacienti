using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionarePacienti.Models
{
    public class StatisticsViewModel
    {
        public List<string> DoctorNames { get; set; }

        public Dictionary<string, int> AppointmentsPerDoctor{ get; set; }

    }
}
