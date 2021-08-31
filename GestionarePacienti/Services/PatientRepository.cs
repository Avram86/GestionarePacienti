using GestionarePacienti.Data;
using Microsoft.Extensions.Logging;
using GestionarePacienti.Data.Entities;

namespace GestionarePacienti.Services
{
    public class PatientRepository:Repository<Patient>
    {
        public PatientRepository(GestionarePacientiContext context, ILogger<PatientRepository> logger)
            :base(context, logger)
        {

        }
    }
}
