using GestionarePacienti.Models;
using GestionarePacienti.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionarePacienti.Services
{
    public class DoctorRepository:Repository<Doctor>
    {
        public DoctorRepository(GestionarePacientiContext context, ILogger<DoctorRepository> logger)
           : base(context, logger)
        {

        }
    }
}
