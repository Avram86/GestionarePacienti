using GestionarePacienti.Data;
using Microsoft.Extensions.Logging;
using GestionarePacienti.Data.Entities;

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
